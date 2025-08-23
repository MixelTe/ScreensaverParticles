using System.Globalization;

namespace ScreenSaverParticles;

public partial class Form3 : Form
{
	private readonly int _controlsWidth = 40;
	private readonly Bitmap _bitmap;
	private readonly Graphics _g;
	private readonly ResultViewer _resultViewer;
	private readonly BrightnessInput _brightnessInput;
	private readonly HueInput _hueInput;

	public Form3()
	{
		InitializeComponent();
		if (CultureInfo.CurrentUICulture.Name == "ru-RU")
		{
			Text = "Screensaver: цвет точек";
			OKbutton.Text = "ОК";
			Cancelbutton.Text = "Отмена";
		}
		_bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
		pictureBox1.Image = _bitmap;
		_g = Graphics.FromImage(_bitmap);

		var resultViewerRect = new Rectangle(0, 0, pictureBox1.Image.Width - _controlsWidth, pictureBox1.Image.Height - _controlsWidth);
		var rangeInputHRect = new Rectangle(0, pictureBox1.Image.Height - _controlsWidth, _controlsWidth, pictureBox1.Image.Width - _controlsWidth);
		var rangeInputLRect = new Rectangle(pictureBox1.Image.Width - _controlsWidth, 0, _controlsWidth, pictureBox1.Image.Height - _controlsWidth);

		_resultViewer = new ResultViewer(resultViewerRect);
		_hueInput = new HueInput(rangeInputHRect);
		_brightnessInput = new BrightnessInput(rangeInputLRect);

		_hueInput.AddOnChangeListener(() => { _resultViewer.Draw(_g); });
		_brightnessInput.AddOnChangeListener(() => { _resultViewer.Draw(_g); });
	}
	private void Form3_Load(object sender, EventArgs e)
	{
		DrawAll();
	}

	private void OKbutton_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		_hueInput.Save();
		_brightnessInput.Save();
	}

	private void DrawAll()
	{
		_resultViewer.Draw(_g);
		_hueInput.Draw(_g);
		_brightnessInput.Draw(_g);
	}

	private bool _mouseDown = false;
	private Point _pastLocation = new();
	private RangeInput? _selectedEl;
	private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
	{
		_mouseDown = true;
		_pastLocation = e.Location;
		if (_hueInput._rect.IntersectPoint(_pastLocation)) _selectedEl = _hueInput;
		else if (_brightnessInput._rect.IntersectPoint(_pastLocation)) _selectedEl = _brightnessInput;
		else _selectedEl = null;
		_selectedEl?.MouseDown(_pastLocation);
	}
	private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
	{
		_mouseDown = false;
		if (_selectedEl != null)
		{
			_selectedEl.MouseUp(e.Location);
			_selectedEl.Draw(_g);
			pictureBox1.Image = _bitmap;
		}
	}
	private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
	{
		if (_mouseDown && _selectedEl != null)
		{
			var l = e.Location;

			var dx = Location.X - _pastLocation.X;
			var dy = Location.Y - _pastLocation.Y;

			_selectedEl.MouseMove(l, dx, dy);
			_selectedEl.Draw(_g);
			pictureBox1.Image = _bitmap;

			_pastLocation = l;
		}
	}
}

class HueInput : RangeInput
{
	private readonly Bitmap _background;

	public HueInput(Rectangle rect) : base(rect, 360, false, 2)
	{
		_handles[0].Value = Program.Settings.ColorMin;
		_handles[1].Value = Program.Settings.ColorMax;
		AddOnChangeListener(ChangeResultViewer);
		_background = CreateBackground();
	}
	private void ChangeResultViewer()
	{
		ResultViewer.ColorMin = _handles[0].ValueWithD;
		ResultViewer.ColorMax = _handles[1].ValueWithD;
	}

	public void Save()
	{
		Program.Settings.ColorMin = _handles[0].Value;
		Program.Settings.ColorMax = _handles[1].Value;
	}

	public override void Draw(Graphics g)
	{
		DrawBackground(g);
		using (var brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
		{
			g.FillRectangle(brush, _rectLine);
		}
		for (int i = 0; i < _handles.Length; i++)
		{
			_handles[i].Draw(g);
		}
	}

	private Bitmap CreateBackground()
	{
		var tileWidth = 2;
		var background = new Bitmap(_rect.Width, _rect.Height);
		using var g = Graphics.FromImage(background);
		g.FillRectangle(Brushes.White, _rect);
		var titleCount = _rect.Width / tileWidth;
		var colorStep = 360f / titleCount;
		using var brush = new SolidBrush(Color.Black);
		for (int i = 0; i < titleCount; i++)
		{
			brush.Color = new HSL((int)(colorStep * i), 100, 50).HSLToRGB().RGBToColor(255);
			g.FillRectangle(brush, tileWidth * i, 0, tileWidth, _rect.Height);
		}
		return background;
	}
	private void DrawBackground(Graphics g)
	{
		g.DrawImage(_background, _rect.X, _rect.Y);
	}
}
class BrightnessInput : RangeInput
{
	private readonly Bitmap _background;
	public BrightnessInput(Rectangle rect) : base(rect, 100, true, 2)
	{
		_handles[0].Value = (int)Math.Round(Program.Settings.ColorLMin * 100);
		_handles[1].Value = (int)Math.Round(Program.Settings.ColorLMax * 100);
		AddOnChangeListener(ChangeResultViewer);
		_background = CreateBackground();
	}
	private void ChangeResultViewer()
	{
		ResultViewer.ColorLMin = _handles[0].ValueWithD / 100f;
		ResultViewer.ColorLMax = _handles[1].ValueWithD / 100f;
	}

	public void Save()
	{
		Program.Settings.ColorLMin = _handles[0].Value / 100f;
		Program.Settings.ColorLMax = _handles[1].Value / 100f;
	}
	public override void Draw(Graphics g)
	{
		DrawBackground(g);
		using (var brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
		{
			g.FillRectangle(brush, _rectLine);
		}
		for (int i = 0; i < _handles.Length; i++)
		{
			_handles[i].Draw(g);
		}
	}
	private Bitmap CreateBackground()
	{
		var tileWidth = 2;
		var background = new Bitmap(_rect.Width, _rect.Height);
		using var g = Graphics.FromImage(background);
		g.FillRectangle(Brushes.White, _rect);
		var titleCount = _rect.Height / tileWidth;
		var colorStep = 255f / titleCount;
		using var brush = new SolidBrush(Color.Black);
		for (int i = 0; i < titleCount; i++)
		{
			var c = (int)(colorStep * i);
			brush.Color = Color.FromArgb(255, c, c, c);
			g.FillRectangle(brush, 0, tileWidth * i, _rect.Width, tileWidth);
		}
		return background;
	}
	private void DrawBackground(Graphics g)
	{
		g.DrawImage(_background, _rect.X, _rect.Y);
	}
}

class ResultViewer
{
	public static int ColorMin = Program.Settings.ColorMin;
	public static int ColorMax = Program.Settings.ColorMax;
	public static float ColorLMin = Program.Settings.ColorLMin;
	public static float ColorLMax = Program.Settings.ColorLMax;

	private readonly Point[] _points;
	public readonly Rectangle _Rect;

	public ResultViewer(Rectangle rect)
	{
		ColorMin = Program.Settings.ColorMin;
		ColorMax = Program.Settings.ColorMax;
		ColorLMin = Program.Settings.ColorLMin;
		ColorLMax = Program.Settings.ColorLMax;
		_Rect = rect;
		_points = CreatePoints();
	}
	private Point[] CreatePoints()
	{
		var pointsCount = _Rect.Width * _Rect.Height / (100 * 100) * Program.Settings.Density;
		var points = new Point[pointsCount];
		var width = (_Rect.Width - Program.Settings.PointRadius * 2) / (float)pointsCount;
		var height = (_Rect.Height - Program.Settings.PointRadius * 2) / (float)pointsCount;
		var xPoints = new int[pointsCount];
		for (int i = 0; i < pointsCount; i++)
			xPoints[i] = _Rect.X + (int)(i * width);
		xPoints.Shuffle();
		for (int i = 0; i < pointsCount; i++)
		{
			var pRect = new Rectangle(xPoints[i], _Rect.Y + (int)(i * height), (int)width, (int)height);
			//var x = Random.Shared.Next(_Rect.Width - Program.Settings.PointRadius * 2) + _Rect.X + Program.Settings.PointRadius;
			//var y = Random.Shared.Next(_Rect.Height - Program.Settings.PointRadius * 2) + _Rect.Y + Program.Settings.PointRadius;
			var x = Random.Shared.Next(pRect.Width) + pRect.X;
			var y = Random.Shared.Next(pRect.Height) + pRect.Y;
			points[i] = new Point(x, y);
		}
		return points;
	}
	public void Draw(Graphics g)
	{
		using var brush = new SolidBrush(Program.Settings.BackgroundColor);
		g.FillRectangle(brush, _Rect);
		g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		foreach (var point in _points)
		{
			var h = Random.Shared.Next(ColorMin, ColorMax);
			var l = Random.Shared.Next((int)(ColorLMin * 100), (int)(ColorLMax * 100));
			brush.Color = new HSL(h, 100, l).HSLToRGB().RGBToColor(255);
			g.FillEllipse(brush,
				point.X,
				point.Y,
				Program.Settings.PointRadius * 2,
				Program.Settings.PointRadius * 2);
		}

		g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
	}
}
class RangeInput
{
	private readonly int _offset = 10;
	private readonly int _width = 6;
	private readonly int _handleZone = 3;


	private readonly int _maxValue;
	protected readonly Handle[] _handles;
	private readonly float _valueDrawStep;
	public readonly Rectangle _rect;
	protected readonly Rectangle _rectLine;
	private readonly bool _vertical;

	public RangeInput(Rectangle rect, int maxValue, bool vertical, int handlesCount)
	{
		_maxValue = maxValue;
		var x = rect.X;
		var y = rect.Y;
		var width = rect.Width;
		var lenght = rect.Height;
		_vertical = vertical;
		_handles = new Handle[handlesCount];
		_valueDrawStep = (float)(lenght - _offset * 2) / _maxValue;
		var lineAdd = (width - _width) / 2;
		if (vertical)
		{
			_rect = new Rectangle(x, y, width, lenght);
			_rectLine = new Rectangle(x + lineAdd, y + _offset, _width, lenght - _offset * 2);
		}
		else
		{
			_rect = new Rectangle(x, y, lenght, width);
			_rectLine = new Rectangle(x + _offset, y + lineAdd, lenght - _offset * 2, _width);
		}
		var hStep = handlesCount > 0 ? _maxValue / handlesCount : 0;
		for (int i = 0; i < handlesCount; i++)
		{
			_handles[i] = new Handle(_maxValue, _rectLine, vertical, _valueDrawStep, OnChange)
			{
				Value = hStep * i
			};
		}
	}

	private List<SomeFunctions.OnChange> _onChanges = [];
	private void OnChange()
	{
		_onChanges.ForEach((SomeFunctions.OnChange f) => { f(); });
	}
	public void AddOnChangeListener(SomeFunctions.OnChange f)
	{
		_onChanges.Add(f);
	}

	public virtual void Draw(Graphics g)
	{
		g.FillRectangle(Brushes.DarkBlue, _rect);
		g.FillRectangle(Brushes.DarkOliveGreen, _rectLine);
		for (int i = 0; i < _handles.Length; i++)
		{
			_handles[i].Draw(g);
		}
	}

	private int _selectedHandle = -1;
	public void MouseDown(Point p)
	{
		for (int i = 0; i < _handles.Length; i++)
		{
			var h = _handles[i];
			var rect = h._rectHandel.Copy();
			rect.Inflate(_handleZone, _handleZone);
			if (rect.IntersectPoint(p))
			{
				_selectedHandle = i;
				h.MouseDown(p);
				break;
			}
		}
		if (_selectedHandle > -1)
		{
			var boundMin = 0;
			var boundMax = _maxValue;
			var H = _handles[_selectedHandle];
			if (_handles.Length > 1)
			{
				var sh = _handles[_selectedHandle].Value;
				for (int i = 0; i < _handles.Length; i++)
				{
					if (_handles[i] == H) continue;
					var h = _handles[i].Value;
					var saveShift = (int)((_handles[i]._handelWidth + _handleZone / 2) / _handles[i]._valueDrawStep);
					if (h < sh)
					{
						//if (h >= boundMin) boundMin = h + 1;
						if (h >= boundMin) boundMin = h + saveShift;
					}
					else
					{
						//if (h <= boundMax) boundMax = h - 1;
						if (h <= boundMax) boundMax = h - saveShift;
					}
				}
			}
			H.SetBounds(boundMin, boundMax);
		}
	}
	public void MouseMove(Point p, int dx, int dy)
	{
		if (_selectedHandle > -1)
		{
			_handles[_selectedHandle].MouseMove(p);
		}
	}
	public void MouseUp(Point p)
	{
		if (_selectedHandle > -1) _handles[_selectedHandle].MouseUp();
		_selectedHandle = -1;
	}
}
class Handle
{
	public readonly int _handelWidth = 6;
	private readonly int _handelHeight = 20;

	public int Value
	{
		get { return _value; }
		set
		{
			_value = Math.Max(Math.Min(value, _maxValue), 0);
			MoveHandle();
		}
	}

	public int ValueWithD
	{
		get { return _value + _dValue; }
	}


	private int _boundMin = -1;
	private int _boundMax = -1;
	private int _value = 0;
	private readonly int _maxValue;
	private readonly Rectangle _rectLine;
	public Rectangle _rectHandel;
	private bool _valueChanging = false;
	private Point _startPoint;
	private int _dValue = 0;
	private readonly bool _vertical;
	public readonly float _valueDrawStep;
	private readonly SomeFunctions.OnChange _onChange;

	public Handle(int maxValue, Rectangle rectLine, bool vertical, float valueDrawStep, SomeFunctions.OnChange onChange)
	{
		_onChange = onChange;
		_maxValue = maxValue;
		_rectLine = rectLine;
		_vertical = vertical;
		_valueDrawStep = valueDrawStep;
		if (vertical) _rectHandel.Size = new Size(_handelHeight, _handelWidth);
		else _rectHandel.Size = new Size(_handelWidth, _handelHeight);
		MoveHandle();
	}
	public void SetBounds(int min, int max)
	{
		_boundMin = min;
		_boundMax = max;
	}

	public void Draw(Graphics g)
	{
		g.FillRectangle(Brushes.Gray, _rectHandel);
		g.DrawRectangle(Pens.White, _rectHandel);
	}

	public void MouseDown(Point p)
	{
		_valueChanging = true;
		_startPoint = p;
		_dValue = 0;
	}
	public void MouseMove(Point p)
	{
		if (_valueChanging)
		{
			int d;
			if (_vertical) d = p.Y - _startPoint.Y;
			else d = p.X - _startPoint.X;

			var pastValue = _dValue;

			_dValue = (int)Math.Round(d / _valueDrawStep);
			_dValue = Math.Max(Math.Min(_dValue, _maxValue - _value), -_value);
			if (_boundMin > -1 && _boundMax > -1)
			{
				_dValue = Math.Max(Math.Min(_dValue, _boundMax - _value), -_value + _boundMin);
			}
			if (pastValue != _dValue) _onChange();
			MoveHandle();
		}
	}
	public void MouseUp()
	{
		_valueChanging = false;
		_value += _dValue;
		_dValue = 0;
	}
	private void MoveHandle()
	{
		var offset = (int)Math.Round((_value + _dValue) * _valueDrawStep);
		if (_vertical)
		{
			_rectHandel.Location = new Point(
				_rectLine.X + _rectLine.Width / 2 - _handelHeight / 2,
				_rectLine.Y - _handelWidth / 2 + offset);
		}
		else
		{
			_rectHandel.Location = new Point(
				_rectLine.X - _handelWidth / 2 + offset,
				_rectLine.Y + _rectLine.Height / 2 - _handelHeight / 2);
		}
	}

}
static class SomeFunctions
{
	public delegate void OnChange();
	public static bool IntersectPoint(this Rectangle rect, Point point)
	{
		return rect.X + rect.Width >= point.X &&
			point.X >= rect.X &&
			rect.Y + rect.Height >= point.Y &&
			point.Y >= rect.Y;
	}
	public static Rectangle Copy(this Rectangle rect)
	{
		return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
	}
}
