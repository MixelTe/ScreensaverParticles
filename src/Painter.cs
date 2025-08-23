namespace ScreenSaverParticles;

class Painter(Controller game, Rectangle rcClient) : IPainter
{
	private readonly Controller _game = game;
	private readonly Rectangle _rcClient = rcClient;
	private readonly Color _c = Color.FromArgb(100, Color.White);

	public void Draw(IGraphics g)
	{
		g.FillRectangle(Program.Settings.BackgroundColor, _rcClient);
		g.SetHighQuality();
		if (Program.Settings.DEV_Presentation)
		{
			for (int i = 0; i < _game.Points.Length; i++)
			{
				var p = _game.Points[i];
				if (p != null && (i * 2) % _game.Points.Length > Program.Settings.DEV_Presentation_VisibleI)
				{
					p.Alpha = Program.Settings.DEV_Presentation_VisibleAlpha;
					if (!p.Visible) p.Alpha = 0;
				}
			}
		}
		
		if (Program.Settings.DrawConections)
			_game.DrawConnections(g);
		
		if (Program.Settings.DrawPoints)
			foreach (var p in _game.Points)
				p?.Draw(g);
		
		foreach (var rect in Program.rectangles)
			g.FillRectangle(_c, rect);
	}


	public void Dispose()
	{
	}
}