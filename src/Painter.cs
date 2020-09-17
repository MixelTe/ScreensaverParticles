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
			g.FillRectangle(Brushes.Blue, _game.Rect);
		}


		public void Dispose()
		{

		}
	}
}