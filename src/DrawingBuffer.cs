using System;
using System.Drawing;

namespace ScreenSaverConections
{
	class DrawingBuffer : IDrawingBuffer, IDisposable
	{
		private readonly BufferedGraphics _BufferedGraphics;
		private IGraphics _graphics;

		public DrawingBuffer(Graphics g, Rectangle rc)
		{
			_BufferedGraphics = BufferedGraphicsManager.Current.Allocate(g, rc);
			_graphics = new StdGraphics(_BufferedGraphics.Graphics);
		}
		public void Dispose()
		{
			_BufferedGraphics?.Dispose();
			_graphics?.Dispose();
		}
		public void Draw(IPainter painter)
		{
			painter.Draw(_graphics);
		}
		public void RenderTo(Graphics target)
		{
			_BufferedGraphics.Render(target);
		}
	}

	class StdGraphics : IGraphics
	{
		private Graphics _graphics;
		private readonly SolidBrush _brush = new SolidBrush(Color.Black);
		private readonly Pen _pen = new Pen(Color.Black);

		public StdGraphics(Graphics graphics)
		{
			_graphics = graphics;
		}

		public void SetHighQuality()
		{
			_graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		}

		public void DrawLine(Color color, float lineWidth, PointF pt1, PointF pt2) =>
			DrawLine(color, lineWidth, pt1.X, pt1.Y, pt2.X, pt2.Y);
		public void DrawLine(Color color, float lineWidth, float x1, float y1, float x2, float y2)
		{
			_pen.Color = color;
			_pen.Width = lineWidth;
			_graphics.DrawLine(_pen, x1, y1, x2, y2);
		}

		public void FillEllipse(Color color, float x, float y, float radiusX, float radiusY)
		{
			_brush.Color = color;
			_graphics.FillEllipse(_brush, x - radiusX, y - radiusY, radiusX * 2, radiusY * 2);
		}

		public void FillRectangle(Color color, float x, float y, float width, float height)
		{
			_brush.Color = color;
			_graphics.FillRectangle(_brush, x, y, width, height);
		}

		public void FillRectangle(Color color, Rectangle rect)
		{
			_brush.Color = color;
			_graphics.FillRectangle(_brush, rect);
		}

		public void Dispose()
		{
			_brush?.Dispose();
			_pen?.Dispose();
		}
	}
}
