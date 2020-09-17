using System;
using System.Drawing;

namespace ScreenSaverConections
{
	interface IDrawingBuffer : IDisposable
	{
		void Draw(IPainter painter);
		void RenderTo(Graphics target);
	}
	internal interface IPainter : IDisposable
	{
		void Draw(Graphics g);
	}

	internal interface IController : IDisposable
	{
		void Update();
	}
}