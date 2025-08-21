using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScreenSaverConections
{
	class ScreensaverControllerNew : IMainController
	{
		public Control TargetControl;
		private readonly FPS _fps = new FPS();
		private GOGraphics _gfx;
		private GraphicsWindow _window;
		private IController _game;
		private IPainter _painter;
		private bool _paused;

		Control IMainController.TargetControl { set => TargetControl = value; }

		public double GetFps() => _fps.Value;

		public ScreensaverControllerNew()
		{
			RegSerializer.Load(Program.KeyName, Program.Settings);
			var gfx = new GameOverlay.Drawing.Graphics()
			{
				MeasureFPS = true,
				PerPrimitiveAntiAliasing = true,
				TextAntiAliasing = true
			};
			var w = Screen.PrimaryScreen.Bounds.Width;
			var h = Screen.PrimaryScreen.Bounds.Height;
			if (Program.Settings.DEV_Presentation)
			{
				Program.Settings.DEV_ClockFakeTimeMode = true;
				Program.Settings.Density = 20;
				var _w = 540;
				h = (int)((float)h / w * _w);
				w = _w;

			}
			Program.SizeMul = (float)Math.Sqrt(w * h / (1920f * 1080));

			_window = new GraphicsWindow(gfx)
			{
				X = 0,
				Y = 0,
				Width = w,
				Height = h,
				FPS = 30,
				IsTopmost = true,
				IsVisible = true,
			};

			_window.SetupGraphics += SetupGraphics;
			_window.DrawGraphics += DrawGraphics;
			_window.DestroyGraphics += DestroyGraphics;

			var game = new Controller(w, h);
			_game = game;
			_painter = new Painter(game, new System.Drawing.Rectangle(0, 0, w, h));
		}

		private void SetupGraphics(object sender, SetupGraphicsEventArgs e)
		{
			_gfx = new GOGraphics(e.Graphics);
		}

		private void DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
		{
			_gfx.Dispose();
			_gfx = null;
		}

		private void DrawGraphics(object sender, DrawGraphicsEventArgs e)
		{
			if (!_paused)
			{
				Program.Settings.DEV_counter++;
				_game.Update();
			}

			if (_gfx != null) _painter.Draw(_gfx);

			//_fps.Increment();
		}

		public void Start()
		{
			if (Program.Settings.DEV_Presentation) TargetControl.Visible = false;
			_window.Create();
		}

		public void Stop()
		{
			_window.Dispose();
			_game.Dispose();
			_painter.Dispose();
		}

		public void TogglePaused()
		{
			_paused = !_paused;
		}

		public void DrawGame() { }
		public void RecreateGame(System.Drawing.Rectangle rcClient) { }
	}

	class GOGraphics : IGraphics
	{
		private GameOverlay.Drawing.Graphics _gfx;
		private readonly GameOverlay.Drawing.SolidBrush _brush;

		public GOGraphics(GameOverlay.Drawing.Graphics gfx)
		{
			_gfx = gfx;
			_brush = gfx.CreateSolidBrush(0, 0, 0);
		}

		public void SetHighQuality() { }

		public void DrawLine(System.Drawing.Color color, float lineWidth, PointF pt1, PointF pt2) =>
			DrawLine(color, lineWidth, pt1.X, pt1.Y, pt2.X, pt2.Y);
		public void DrawLine(System.Drawing.Color color, float lineWidth, float x1, float y1, float x2, float y2)
		{
			UpdateColor(color);
			_gfx.DrawLine(_brush, x1, y1, x2, y2, lineWidth);
		}

		public void FillEllipse(System.Drawing.Color color, float x, float y, float radiusX, float radiusY)
		{
			UpdateColor(color);
			_gfx.FillEllipse(_brush, x, y, radiusX, radiusY);
		}

		public void FillRectangle(System.Drawing.Color color, System.Drawing.Rectangle rect) =>
			FillRectangle(color, rect.Left, rect.Top, rect.Width, rect.Height);
		public void FillRectangle(System.Drawing.Color color, float x, float y, float width, float height)
		{
			UpdateColor(color);
			_gfx.FillRectangle(_brush, x, y, x + width, y + height);
		}

		public void Dispose()
		{
			_brush?.Dispose();
		}

		private void UpdateColor(System.Drawing.Color color)
		{
			if (_brush == null) return;
			_brush.Color = new GameOverlay.Drawing.Color(color.R, color.G, color.B, color.A);
		}
	}
}
