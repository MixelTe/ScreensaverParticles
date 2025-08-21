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

		public void Draw(IGraphics g)
		{
			g.FillRectangle(Program.Settings.BackgroundColor, _rcClient);
			g.SetHighQuality();
			if (Program.Settings.DrawConections)
			{
				_game?.DrawConnections(g);
			}
			if (Program.Settings.DrawPoints)
			{
				foreach (var p in _game._CPoints)
				{
					p?.Draw(g);
				}
			}
			//var c = Color.FromArgb(100, Color.White);
			//foreach (var rect in Program.rectangles)
			//{
			//	g.FillRectangle(c, rect);
			//}
		}


		public void Dispose()
		{
		}
	}
}