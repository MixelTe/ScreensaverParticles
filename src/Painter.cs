using System.Drawing;

namespace ScreenSaverConections
{
	class Painter : IPainter
	{
		private readonly Settings _Settings;
		private readonly Controller _game;
		private readonly Rectangle _rcClient;

		public Painter(Controller game, Rectangle rcClient)
		{
			_Settings = Program.Settings;
			_game = game;
			_rcClient = rcClient;
		}

		public void Draw(IGraphics g)
		{
			g.FillRectangle(_Settings.BackgroundColor, _rcClient);
			g.SetHighQuality();
			if (_Settings.DrawConections)
			{
				foreach (var p in _game._CPoints)
				{
					if (p != null) p.DrawConnections(g);
				}
			}
			if (_Settings.DrawPoints)
			{
				foreach (var p in _game._CPoints)
				{
					if (p != null) p.DrawPoint(g);
				}
			}
			//var b = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
			//foreach (var rect in Program.rectangles)
			//{
			//	g.FillRectangle(b, rect);
			//}
			//b.Dispose();
		}


		public void Dispose()
		{
		}
	}
}