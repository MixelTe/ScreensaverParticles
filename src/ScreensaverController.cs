using System.Diagnostics;

namespace ScreenSaverParticles;

class ScreensaverController : IMainController
{
	private readonly object _lock = new();
	private IController? _game;
	private IPainter? _painter;
	private bool _paused;
	private IDrawingBuffer? _drawingBuffer;
	private readonly FPS _fps = new();
	private long _time;

	public Control? TargetControl;
	private Thread? _t;
	private int _isRunning;

	public double GetFps() => _fps.Value;
	Control IMainController.TargetControl { set => TargetControl = value; }

	public void TogglePaused()
	{
		lock (_lock) _paused = !_paused;
	}
	private bool Paused { get { lock (_lock) return _paused; } }
	private bool IsRunning
	{
		get => Interlocked.CompareExchange(ref _isRunning, 0, 0) == 1;
		set => Interlocked.Exchange(ref _isRunning, value ? 1 : 0);
	}
	public ScreensaverController()
	{
		RegSerializer.Load(Program.KeyName, Program.Settings);
		_time = Stopwatch.GetTimestamp();
	}
	public void RecreateGame(Rectangle rcClient)
	{
		if (TargetControl != null) 
			TargetControl.BackColor = Program.Settings.BackgroundColor;
		var game = new Controller(rcClient.Width, rcClient.Height);
		var painter = new Painter(game, rcClient);

		AssignComponents(game, painter, rcClient);
	}
	private void AssignComponents(Controller game, Painter painter, Rectangle rcClient)
	{
		lock (_lock)
		{
			_game?.Dispose();
			_painter?.Dispose();
			_game = game;
			_painter = painter;

			_drawingBuffer?.Dispose();
			_drawingBuffer = CreateDrawing1(rcClient);
		}
	}

	private IDrawingBuffer? CreateDrawing1(Rectangle rcClient)
	{
		if (TargetControl == null) return null;
		return new DrawingBuffer(TargetControl.CreateGraphics(), rcClient);
	}
	public void Start()
	{
		_t = new Thread(GameLoop)
		{
			IsBackground = true
		};

		IsRunning = true;
		_t.Start();
	}
	public void Stop()
	{
		IsRunning = false;
		_t?.Join(TimeSpan.FromSeconds(20)); // wait thread to finish
	}
	private void GameLoop()
	{
		while (IsRunning)
		{
			var time = Stopwatch.GetTimestamp();
			if (time - _time <= Stopwatch.Frequency / 30) continue;
			_time = time;
			if (Paused) continue;

			lock (_lock) _game?.Update();

			DrawGame();

			_fps.Increment();
		}
	}
	public void DrawGame()
	{
		if (TargetControl == null || _drawingBuffer == null || _painter == null) return;
		lock (_lock)
		{
			using var gTarget = TargetControl.CreateGraphics();
			_drawingBuffer.Draw(_painter);
			_drawingBuffer.RenderTo(gTarget);
		}
	}
}
