using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class ScreensaverController : IMainController
	{
		private object _lock = new object();
		private IController _game;
		private IPainter _painter;
		private bool _Paused;
		private IDrawingBuffer _DrawingBuffer;
		private readonly FPS _fps = new FPS();
		private long _time;

		public Control TargetControl;
		private Thread _t;
		private int _IsRunning;

		public double GetFps() => _fps.Value;
		Control IMainController.TargetControl { set => TargetControl = value; }


		public void TogglePaused()
		{
			lock (_lock)
			{
				_Paused = !_Paused;
			}
		}
		private bool Paused
		{
			get
			{
				lock (_lock) return _Paused;
			}
		}
		private bool IsRunning
		{
			get => Interlocked.CompareExchange(ref _IsRunning, 0, 0) == 1;
			set => Interlocked.Exchange(ref _IsRunning, value ? 1 : 0);
		}
		public ScreensaverController()
		{
			RegSerializer.Load(Program.KeyName, Program.Settings);
			_time = Stopwatch.GetTimestamp();
		}
		public void RecreateGame(Rectangle rcClient)
		{
			TargetControl.BackColor = Program.Settings.BackgroundColor;
			var game = new Controller(rcClient.Width, rcClient.Height);
			var painter = new Painter(game, rcClient);

			AssignComponents(game, painter, rcClient);
		}
		private void AssignComponents(Controller game, Painter painter, Rectangle rcClient)
		{
			lock (_lock)
			{
				_game?.Dispose();
				_painter?.Dispose();
				_game = game;
				_painter = painter;

				_DrawingBuffer?.Dispose();
				_DrawingBuffer = CreateDrawing1(rcClient);
			}
		}

		private IDrawingBuffer CreateDrawing1(Rectangle rcClient)
		{
			return new DrawingBuffer(TargetControl.CreateGraphics(), rcClient);
		}
		public void Start()
		{
			_t = new Thread(GameLoop)
			{
				IsBackground = true
			};

			IsRunning = true;
			_t.Start();
		}
		public void Stop()
		{
			IsRunning = false;
			_t.Join(TimeSpan.FromSeconds(20)); // wait thread to finish
		}
		private void GameLoop()
		{
			while (IsRunning)
			{
				var time = Stopwatch.GetTimestamp();
				if (time - _time > Stopwatch.Frequency / 30)
				{
					_time = time;
					if (!Paused)
					{
						lock (_lock)
						{
							_game.Update();
						}

						DrawGame();

						_fps.Increment();
					}
				}
				//Thread.Sleep(1000 / 60);
			}
		}
		public void DrawGame()
		{
			lock (_lock)
			{
				using (var gTarget = TargetControl.CreateGraphics())
				{
					_DrawingBuffer.Draw(_painter);
					_DrawingBuffer.RenderTo(gTarget);
				}
			}
		}
	}
}
