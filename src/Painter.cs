using System.Drawing;

namespace ScreenSaverConections
{
	class Painter : IPainter
	{
		private readonly Controller _game;
		private readonly Rectangle _rcClient;

		public Painter(Controller game, Rectangle rcClient)
		{
			_game = game;
			_rcClient = rcClient;
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(Brushes.Black, _rcClient);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			foreach (var p in _game._CPoints)
			{
				if (p != null) p.Draw(g);
			}
		}


		public void Dispose()
		{

		}
	}
}