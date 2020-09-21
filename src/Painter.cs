using System.Drawing;

namespace ScreenSaverConections
{
	class Painter : IPainter
	{
		private readonly Controller _game;
		private readonly Rectangle _rcClient;
		
		private readonly bool _DrawConections = true;
		private readonly bool _DrawPoints = true;
		private readonly SolidBrush _Brush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));

		public Painter(Controller game, Rectangle rcClient)
		{
			_game = game;
			_rcClient = rcClient;
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(_Brush, _rcClient);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			if (_DrawConections)
			{
				foreach (var p in _game._CPoints)
				{
					if (p != null) p.DrawConnections(g);
				}
			}
			if (_DrawPoints)
			{
				foreach (var p in _game._CPoints)
				{
					if (p != null) p.DrawPoint(g);
				}
			}
		}


		public void Dispose()
		{
			_Brush?.Dispose();
		}
	}
}