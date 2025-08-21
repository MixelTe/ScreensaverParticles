using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace ScreenSaverConections
{
	interface IDrawingBuffer : IDisposable
	{
		void Draw(IPainter painter);
		void RenderTo(Graphics target);
	}
	internal interface IPainter : IDisposable
	{
		void Draw(IGraphics g);
	}

	internal interface IController : IDisposable
	{
		void Update();
	}

	interface IMainController
	{
		Control TargetControl { set; }
		double GetFps();
		void Start();
		void Stop();
		void TogglePaused();
		void DrawGame();
		void RecreateGame(Rectangle rcClient) ;
	}

	internal interface IGraphics : IDisposable
	{
		void SetHighQuality();
		void FillRectangle(Color color, float x, float y, float width, float height);
		void FillRectangle(Color color, Rectangle rect);
		void DrawLine(Color color, float lineWidth, PointF pt1, PointF pt2);
		void FillEllipse(Color color, float x, float y, float width, float height);
	}
}