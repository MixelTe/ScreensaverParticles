using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverConections
{
	class Controller: IController
	{
		private readonly ScreensaverSettings _Settings;
		private readonly int _Height;
		private readonly int _Width;
		public Rectangle Rect;

		private readonly int _Speed;

		public Controller(int width, int height, ScreensaverSettings settings)
		{
			_Settings = settings;
			_Speed = _Settings.Speed;
			_Width = width;
			_Height = height;
			Rect = new Rectangle(width / 2, height / 2, 0, 0);
		}
		public void Update()
		{
			Rect.Inflate(_Speed, _Speed);
			if (Rect.Width > _Width && Rect.Height > _Height)
			{
				Rect = new Rectangle(_Width / 4, _Height / 4, _Width / 2, _Height / 2);
			}
		}

		public void Dispose()
		{

		}
	}
}