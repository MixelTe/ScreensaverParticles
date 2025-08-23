namespace ScreenSaverParticles;

public partial class Form1 : Form
{
	private Point _pastMousePos;
	private bool _firstMouseMove = true;
	private Size _oldSize;
	private readonly IMainController _controller;

	public Form1()
	{
		InitializeComponent();
		Cursor.Hide();
		var newController = true;
		if (newController)
		{
			_controller = new ScreensaverControllerNew();
			timer1.Enabled = false;
		}
		else
		{
			_controller = new ScreensaverController();
		}
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		_controller.TargetControl = this;

		RecreateGame();

		_controller.Start();
	}
	private void Form1_Click(object sender, EventArgs e)
	{
		_controller.TogglePaused();
	}
	private void Timer1_Tick(object sender, EventArgs e)
	{
		if (_oldSize != ClientRectangle.Size)
		{
			RecreateGame();

			_controller.DrawGame();
		}

		//Debug.WriteLine(_Controller.GetFps());
		Text = $"fps={_controller.GetFps():#,##0}";
	}
	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		_controller.Stop();
	}
	private void RecreateGame()
	{
		var rcClient = ClientRectangle;

		_controller.RecreateGame(rcClient);

		_oldSize = rcClient.Size;
	}

	private void Form1_KeyDown(object sender, KeyEventArgs e)
	{
		Close();
	}

	private void Form1_MouseMove(object sender, MouseEventArgs e)
	{
		var mousePos = e.Location;
		if (_firstMouseMove)
		{
			_pastMousePos = mousePos;
			_firstMouseMove = false;
		}
		else
		{
			if (_pastMousePos != mousePos)
			{
				Close();
				_pastMousePos = mousePos;
			}
		}
	}
}
