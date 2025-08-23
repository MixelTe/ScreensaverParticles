namespace ScreenSaverParticles;

interface IDrawingBuffer : IDisposable
{
	void Draw(IPainter painter);
	void RenderTo(Graphics target);
}
public interface IPainter : IDisposable
{
	void Draw(IGraphics g);
}

public interface IController : IDisposable
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

public interface IGraphics : IDisposable
{
	void SetHighQuality();
	void FillRectangle(Color color, float x, float y, float width, float height);
	void FillRectangle(Color color, Rectangle rect);
	void DrawLine(Color color, float lineWidth, PointF pt1, PointF pt2);
	void DrawLine(Color color, float lineWidth, float x1, float y1, float x2, float y2);
	void FillEllipse(Color color, float x, float y, float radiusX, float radiusY);
}