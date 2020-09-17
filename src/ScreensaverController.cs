﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class ScreensaverController
	{
		private readonly ScreensaverSettings _ScreensaverSettings = new ScreensaverSettings();
		private object _lock = new object();
		private IController _game;
		private IPainter _painter;
		private bool _Paused;
		private IDrawingBuffer _DrawingBuffer;
		private readonly FPS _fps = new FPS();

		internal Control TargetControl;
		private Thread _t;
		private bool _IsRunning;

		internal double GetFps() => _fps.Value;


		internal void TogglePaused()
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
			get
			{
				lock (_lock) return _IsRunning;
			}
			set
			{
				lock (_lock) _IsRunning = value;
			}
		}
		public ScreensaverController()
		{
			_ScreensaverSettings.Load();
		}
		internal void RecreateGame(Rectangle rcClient)
		{
			var game = new Controller(rcClient.Width, rcClient.Height, _ScreensaverSettings);
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
		internal void Start()
		{
			_t = new Thread(GameLoop)
			{
				IsBackground = true
			};

			IsRunning = true;
			_t.Start();
		}
		internal void Stop()
		{
			IsRunning = false;
			_t.Join(TimeSpan.FromSeconds(20)); // wait thread to finish
		}
		private void GameLoop()
		{
			while (IsRunning)
			{
				if (!Paused)
				{
					lock (_lock)
					{
						_game.Update();
					}

					DrawGame();

					_fps.Increment();
				}
				Thread.Sleep(1000 / 30);
			}
		}
		internal void DrawGame()
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
