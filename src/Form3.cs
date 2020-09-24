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
		private RangeInput _RangeInputL;
		private RangeInput _RangeInputH;

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

			_ResultViewer = new ResultViewer(0, 0, pictureBox1.Image.Width - _ControlsWidth, pictureBox1.Image.Height - _ControlsWidth);
			_RangeInputH = new RangeInput(0, pictureBox1.Image.Height - _ControlsWidth, _ControlsWidth, pictureBox1.Image.Width - _ControlsWidth, false, 2);
			_RangeInputL = new RangeInput(pictureBox1.Image.Width - _ControlsWidth, 0, _ControlsWidth, pictureBox1.Image.Height - _ControlsWidth, true, 2);
		}

		private void OKbutton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void DrawAll()
		{
			_ResultViewer.Draw(_G);
			_RangeInputH.Draw(_G);
			_RangeInputL.Draw(_G);
		}

		private bool _MouseDown = false;
		private Point _PastLocation = new Point();
		private RangeInput _SelectedEl;
		private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			_MouseDown = true;
			_PastLocation = e.Location;
			if (_RangeInputH._Rect.IntersectPoint(_PastLocation)) _SelectedEl = _RangeInputH;
			else if (_RangeInputL._Rect.IntersectPoint(_PastLocation)) _SelectedEl = _RangeInputL;
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

	class ResultViewer
	{
		public readonly Rectangle _Rect;
		public ResultViewer(int x, int y, int width, int height)
		{
			_Rect = new Rectangle(x, y, width, height);
		}
		public void Draw(Graphics g)
		{
			g.FillRectangle(Brushes.Blue, _Rect);
		}
	}
	class RangeInput
	{
		private readonly int _Offset = 10;
		private readonly int _Width = 6;
		private readonly int _HandleZone = 3;

		private readonly int MaxValue = 100;
		private readonly Handle[] Handles;

		private readonly float _ValueDrawStep;
		public readonly Rectangle _Rect;
		public readonly Rectangle _RectLine;
		public Rectangle _RectHandel1 = new Rectangle();
		public Rectangle _RectHandel2 = new Rectangle();
		private readonly bool _Vertical;

		public RangeInput(int x, int y, int width, int lenght, bool vertical, int handles)
		{
			_Vertical = vertical;
			Handles = new Handle[handles];
			_ValueDrawStep = (float)(lenght - _Offset * 2) / MaxValue;
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
			var hStep = handles > 0 ? MaxValue / handles : 0;
			for (int i = 0; i < handles; i++)
			{
				Handles[i] = new Handle(MaxValue, _RectLine, vertical, _ValueDrawStep);
				Handles[i].Value = hStep * i;
			}
		}

		public void Draw(Graphics g)
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
				var boundMax = MaxValue;
				var H = Handles[_SelectedHandle];
				if (Handles.Length > 1)
				{
					var sh = Handles[_SelectedHandle].Value;
					for (int i = 0; i < Handles.Length; i++)
					{
						if (Handles[i] == H) continue;
						var h = Handles[i].Value;
						if (h < sh)
						{
							if (h > boundMin) boundMin = h;
						}
						else
						{
							if (h < boundMax) boundMax = h;
						}
					}
				}
				if (boundMin > 0) boundMin++;
				if (boundMax < MaxValue) boundMax--;
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
		private readonly int _HandelWidth = 6;
		private readonly int _HandelHeight = 20;

		public int Value
		{
			get { return _Value; }
			set
			{
				_Value = value;
				MoveHandle();
			}
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
		private readonly float _ValueDrawStep;

		public Handle(int maxValue, Rectangle rectLine, bool vertical, float valueDrawStep)
		{
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
			g.FillRectangle(Brushes.Brown, _RectHandel);
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

				_DValue = (int)Math.Round(d / _ValueDrawStep);
				_DValue = Math.Max(Math.Min(_DValue, _MaxValue - _Value), -_Value);
				if (_BoundMin > -1 && _BoundMax > -1)
				{
					_DValue = Math.Max(Math.Min(_DValue, _BoundMax - _Value), -_Value + _BoundMin);
				}
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
