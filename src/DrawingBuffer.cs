using System;
using System.Drawing;

namespace ScreenSaverConections
{
	class DrawingBuffer : IDrawingBuffer, IDisposable
	{
		private readonly BufferedGraphics _BufferedGraphics;

		public DrawingBuffer(Graphics g, Rectangle rc)
		{
			_BufferedGraphics = BufferedGraphicsManager.Current.Allocate(g, rc);
		}
		public void Dispose()
		{
			_BufferedGraphics?.Dispose();
		}
		public void Draw(IPainter painter)
		{
			painter.Draw(_BufferedGraphics.Graphics);
		}
		public void RenderTo(Graphics target)
		{
			_BufferedGraphics.Render(target);
		}
	}
}
