using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	public partial class Form3 : Form
	{
		private readonly int _ControlsWidth = 40;
		Bitmap _Bitmap;
		private Graphics _G;
		private ResultViewer _ResultViewer;
		private BrightnessInput _BrightnessInput;
		private HueInput _HueInput;

		public Form3()
		{
			InitializeComponent();
		}
		private void Form3_Load(object sender, EventArgs e)
		{
			InitGrafics();
			DrawAll();
		}
		private void InitGrafics()
		{
			_Bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			pictureBox1.Image = _Bitmap;
			_G = Graphics.FromImage(_Bitmap);

			var resultViewerRect = new Rectangle(0, 0, pictureBox1.Image.Width - _ControlsWidth, pictureBox1.Image.Height - _ControlsWidth);
			var rangeInputHRect = new Rectangle(0, pictureBox1.Image.Height - _ControlsWidth, _ControlsWidth, pictureBox1.Image.Width - _ControlsWidth);
			var rangeInputLRect = new Rectangle(pictureBox1.Image.Width - _ControlsWidth, 0, _ControlsWidth, pictureBox1.Image.Height - _ControlsWidth);

			_ResultViewer = new ResultViewer(resultViewerRect);
			_HueInput = new HueInput(rangeInputHRect);
			_BrightnessInput = new BrightnessInput(rangeInputLRect);

			_HueInput.AddOnChangeListener(() => { _ResultViewer.Draw(_G); });
			_BrightnessInput.AddOnChangeListener(() => { _ResultViewer.Draw(_G); });
		}

		private void OKbutton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			_HueInput.Save();
			_BrightnessInput.Save();
		}

		private void DrawAll()
		{
			_ResultViewer.Draw(_G);
			_HueInput.Draw(_G);
			_BrightnessInput.Draw(_G);
		}

		private bool _MouseDown = false;
		private Point _PastLocation = new Point();
		private RangeInput _SelectedEl;
		private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			_MouseDown = true;
			_PastLocation = e.Location;
			if (_HueInput._Rect.IntersectPoint(_PastLocation)) _SelectedEl = _HueInput;
			else if (_BrightnessInput._Rect.IntersectPoint(_PastLocation)) _SelectedEl = _BrightnessInput;
			else _SelectedEl = null;
			if (_SelectedEl != null) _SelectedEl.MouseDown(_PastLocation);
		}
		private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			_MouseDown = false;
			if (_SelectedEl != null) 
			{
				_SelectedEl.MouseUp(e.Location);
				_SelectedEl.Draw(_G);
				pictureBox1.Image = _Bitmap;
			}
		}
		private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (_MouseDown && _SelectedEl != null)
			{
				var l = e.Location;

				var dx = Location.X - _PastLocation.X;
				var dy = Location.Y - _PastLocation.Y;

				_SelectedEl.MouseMove(l, dx, dy);
				_SelectedEl.Draw(_G);
				pictureBox1.Image = _Bitmap;

				_PastLocation = l;
			}
		}
	}

	class HueInput: RangeInput
	{
		public HueInput(Rectangle rect) : base(rect, 360, false, 2)
		{
			Handles[0].Value = Program.Settings.ColorMin;
			Handles[1].Value = Program.Settings.ColorMax;
			AddOnChangeListener(ChangeResultViewer);
			CreateBackground();
		}
		private void ChangeResultViewer()
		{
			ResultViewer.ColorMin = Handles[0].ValueWithD;
			ResultViewer.ColorMax = Handles[1].ValueWithD;
		}

		public void Save()
		{
			Program.Settings.ColorMin = Handles[0].Value;
			Program.Settings.ColorMax = Handles[1].Value;
		}

		public override void Draw(Graphics g)
		{
			DrawBackground(g);
			using (var brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
			{
				g.FillRectangle(brush, _RectLine);
			}
			for (int i = 0; i < Handles.Length; i++)
			{
				Handles[i].Draw(g);
			}
		}

		private readonly int _TileWidth = 2;
		private Bitmap _Background;
		private void CreateBackground()
		{
			_Background = new Bitmap(_Rect.Width, _Rect.Height);
			using (var g = Graphics.FromImage(_Background))
			{
				g.FillRectangle(Brushes.White, _Rect);
				var titleCount = _Rect.Width / _TileWidth;
				var colorStep = 360f / titleCount;
				for (int i = 0; i < titleCount; i++)
				{
					using (var brush = new SolidBrush(new HSL((int)(colorStep * i), 100, 50).HSLToRGB().RGBToColor(255)))
					{
						g.FillRectangle(brush, _TileWidth * i, 0, _TileWidth, _Rect.Height);
					}
				}
			}
		}
		private void DrawBackground(Graphics g)
		{
			g.DrawImage(_Background, _Rect.X, _Rect.Y);
		}
	}
	class BrightnessInput : RangeInput
	{
		public BrightnessInput(Rectangle rect) : base(rect, 100, true, 2)
		{
			Handles[0].Value = (int)Math.Round(Program.Settings.ColorLMin * 100);
			Handles[1].Value = (int)Math.Round(Program.Settings.ColorLMax * 100);
			AddOnChangeListener(ChangeResultViewer);
			CreateBackground();
		}
		private void ChangeResultViewer()
		{
			ResultViewer.ColorLMin = Handles[0].ValueWithD / 100f;
			ResultViewer.ColorLMax = Handles[1].ValueWithD / 100f;
		}

		public void Save()
		{
			Program.Settings.ColorLMin = Handles[0].Value / 100f;
			Program.Settings.ColorLMax = Handles[1].Value / 100f;
		}
		public override void Draw(Graphics g)
		{
			DrawBackground(g);
			using (var brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0)))
			{
				g.FillRectangle(brush, _RectLine);
			}
			for (int i = 0; i < Handles.Length; i++)
			{
				Handles[i].Draw(g);
			}
		}
		private readonly int _TileWidth = 2;
		private Bitmap _Background;
		private void CreateBackground()
		{
			_Background = new Bitmap(_Rect.Width, _Rect.Height);
			using (var g = Graphics.FromImage(_Background))
			{
				g.FillRectangle(Brushes.White, _Rect);
				var titleCount = _Rect.Height / _TileWidth;
				var colorStep = 255f / titleCount;
				for (int i = 0; i < titleCount; i++)
				{
					var c = (int)(colorStep * i);
					using (var brush = new SolidBrush(Color.FromArgb(255, c, c, c)))
					{
						g.FillRectangle(brush, 0, _TileWidth * i, _Rect.Width, _TileWidth);
					}
				}
			}
		}
		private void DrawBackground(Graphics g)
		{
			g.DrawImage(_Background, _Rect.X, _Rect.Y);
		}
	}

	class ResultViewer
	{
		public static int ColorMin = Program.Settings.ColorMin;
		public static int ColorMax = Program.Settings.ColorMax;
		public static float ColorLMin = Program.Settings.ColorLMin;
		public static float ColorLMax = Program.Settings.ColorLMax;

		private Point[] _Points;

		public readonly Rectangle _Rect;
		public ResultViewer(Rectangle rect)
		{
			ColorMin = Program.Settings.ColorMin;
			ColorMax = Program.Settings.ColorMax;
			ColorLMin = Program.Settings.ColorLMin;
			ColorLMax = Program.Settings.ColorLMax;
			_Rect = rect;
			CreatePoints();
		}
		private void CreatePoints()
		{
			var rnd = new Random();
			var pointsCount = _Rect.Width * _Rect.Height / (100 * 100) * Program.Settings.Density;
			_Points = new Point[pointsCount];
			var width = (_Rect.Width - Program.Settings.PointRadius * 2) / (float)pointsCount;
			var height = (_Rect.Height - Program.Settings.PointRadius * 2) / (float)pointsCount;
			var xPoints = new int[pointsCount];
			for (int i = 0; i < pointsCount; i++)
			{
				xPoints[i] = _Rect.X + (int)(i * width);
			}
			xPoints.Shuffle();
			for (int i = 0; i < pointsCount; i++)
			{
				var pRect = new Rectangle(xPoints[i], _Rect.Y + (int)(i * height), (int)width, (int)height);
				//var x = rnd.Next(_Rect.Width - Program.Settings.PointRadius * 2) + _Rect.X + Program.Settings.PointRadius;
				//var y = rnd.Next(_Rect.Height - Program.Settings.PointRadius * 2) + _Rect.Y + Program.Settings.PointRadius;
				var x = rnd.Next(pRect.Width) + pRect.X;
				var y = rnd.Next(pRect.Height) + pRect.Y;
				_Points[i] = new Point(x, y);
			}
		}
		public void Draw(Graphics g)
		{
			using (var brush = new SolidBrush(Program.Settings.BackgroundColor))
			{
				g.FillRectangle(brush, _Rect);
			}
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			var rnd = new Random();
			foreach (var point in _Points)
			{
				var h = rnd.Next(ColorMin, ColorMax);
				var l = rnd.Next((int)(ColorLMin * 100), (int)(ColorLMax * 100));
				using (var brush = new SolidBrush(new HSL(h, 100, l).HSLToRGB().RGBToColor(255)))
				{
					g.FillEllipse(brush,
						point.X,
						point.Y,
						Program.Settings.PointRadius,
						Program.Settings.PointRadius);
				}
			}

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
		}
	}
	class RangeInput
	{
		private readonly int _Offset = 10;
		private readonly int _Width = 6;
		private readonly int _HandleZone = 3;


		private readonly int _MaxValue;
		protected readonly Handle[] Handles;
		private readonly float _ValueDrawStep;
		public readonly Rectangle _Rect;
		protected readonly Rectangle _RectLine;
		private readonly bool _Vertical;

		public RangeInput(Rectangle rect, int maxValue, bool vertical, int handlesCount)
		{
			_MaxValue = maxValue;
			var x = rect.X;
			var y = rect.Y;
			var width = rect.Width;
			var lenght = rect.Height;
			_Vertical = vertical;
			Handles = new Handle[handlesCount];
			_ValueDrawStep = (float)(lenght - _Offset * 2) / _MaxValue;
			var lineAdd = (width - _Width) / 2;
			if (vertical)
			{
				_Rect = new Rectangle(x, y, width, lenght);
				_RectLine = new Rectangle(x + lineAdd, y + _Offset, _Width, lenght - _Offset * 2);
			}
			else 
			{ 
				_Rect = new Rectangle(x, y, lenght, width);
				_RectLine = new Rectangle(x + _Offset, y + lineAdd, lenght - _Offset * 2, _Width); 
			}
			var hStep = handlesCount > 0 ? _MaxValue / handlesCount : 0;
			for (int i = 0; i < handlesCount; i++)
			{
				Handles[i] = new Handle(_MaxValue, _RectLine, vertical, _ValueDrawStep, OnChange)
				{
					Value = hStep * i
				};
			}
		}

		private List<SomeFunctions.OnChange> _OnChanges = new List<SomeFunctions.OnChange>();
		private void OnChange()
		{
			_OnChanges.ForEach((SomeFunctions.OnChange f) => { f(); });
		}
		public void AddOnChangeListener(SomeFunctions.OnChange f)
		{
			_OnChanges.Add(f);
		}

		public virtual void Draw(Graphics g)
		{
			g.FillRectangle(Brushes.DarkBlue, _Rect);
			g.FillRectangle(Brushes.DarkOliveGreen, _RectLine);
			for (int i = 0; i < Handles.Length; i++)
			{
				Handles[i].Draw(g);
			}
		}

		private int _SelectedHandle = -1;
		public void MouseDown(Point p)
		{
			for (int i = 0; i < Handles.Length; i++)
			{
				var h = Handles[i];
				var rect = h._RectHandel.Copy();
				rect.Inflate(_HandleZone, _HandleZone);
				if (rect.IntersectPoint(p))
				{
					_SelectedHandle = i;
					h.MouseDown(p);
					break;
				}
			}
			if (_SelectedHandle > -1)
			{
				var boundMin = 0;
				var boundMax = _MaxValue;
				var H = Handles[_SelectedHandle];
				if (Handles.Length > 1)
				{
					var sh = Handles[_SelectedHandle].Value;
					for (int i = 0; i < Handles.Length; i++)
					{
						if (Handles[i] == H) continue;
						var h = Handles[i].Value;
						var saveShift = (int)((Handles[i]._HandelWidth + _HandleZone / 2) / Handles[i]._ValueDrawStep);
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
			if (_SelectedHandle > -1)
			{
				Handles[_SelectedHandle].MouseMove(p);
			}
		}
		public void MouseUp(Point p)
		{
			if (_SelectedHandle > -1) Handles[_SelectedHandle].MouseUp();
			_SelectedHandle = -1;
		}
	}
	class Handle
	{
		public readonly int _HandelWidth = 6;
		private readonly int _HandelHeight = 20;

		public int Value
		{
			get { return _Value; }
			set
			{
				_Value = Math.Max(Math.Min(value, _MaxValue), 0);
				MoveHandle();
			}
		}

		public int ValueWithD
		{
			get { return _Value + _DValue; }
		}


		private int _BoundMin = -1;
		private int _BoundMax = -1;
		private int _Value = 0;
		private readonly int _MaxValue;
		private readonly Rectangle _RectLine;
		public Rectangle _RectHandel = new Rectangle();
		private bool _ValueChanging = false;
		private Point StartPoint;
		private int _DValue = 0;
		private readonly bool _Vertical;
		public readonly float _ValueDrawStep;
		private readonly SomeFunctions.OnChange _OnChange;

		public Handle(int maxValue, Rectangle rectLine, bool vertical, float valueDrawStep, SomeFunctions.OnChange onChange)
		{
			_OnChange = onChange;
			_MaxValue = maxValue;
			_RectLine = rectLine;
			_Vertical = vertical;
			_ValueDrawStep = valueDrawStep;
			if (vertical) _RectHandel.Size = new Size(_HandelHeight, _HandelWidth);
			else _RectHandel.Size = new Size(_HandelWidth, _HandelHeight);
			MoveHandle();
		}
		public void SetBounds(int min, int max)
		{
			_BoundMin = min;
			_BoundMax = max;
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(Brushes.Gray, _RectHandel);
			g.DrawRectangle(Pens.White, _RectHandel);
		}

		public void MouseDown(Point p)
		{
			_ValueChanging = true;
			StartPoint = p;
			_DValue = 0;
		}
		public void MouseMove(Point p)
		{
			if (_ValueChanging)
			{
				int d;
				if (_Vertical) d = p.Y - StartPoint.Y;
				else d = p.X - StartPoint.X;

				var pastValue = _DValue;

				_DValue = (int)Math.Round(d / _ValueDrawStep);
				_DValue = Math.Max(Math.Min(_DValue, _MaxValue - _Value), -_Value);
				if (_BoundMin > -1 && _BoundMax > -1)
				{
					_DValue = Math.Max(Math.Min(_DValue, _BoundMax - _Value), -_Value + _BoundMin);
				}
				if (pastValue != _DValue) _OnChange();
				MoveHandle();
			}
		}
		public void MouseUp()
		{
			_ValueChanging = false;
			_Value += _DValue;
			_DValue = 0;
		}
		private void MoveHandle()
		{
			var offset = (int)Math.Round((_Value + _DValue) * _ValueDrawStep);
			if (_Vertical)
			{
				_RectHandel.Location = new Point(
					_RectLine.X + _RectLine.Width / 2 - _HandelHeight / 2,
					_RectLine.Y - _HandelWidth / 2 + offset);
			}
			else
			{
				_RectHandel.Location = new Point(
					_RectLine.X - _HandelWidth / 2 + offset,
					_RectLine.Y + _RectLine.Height / 2 - _HandelHeight / 2);
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
}
