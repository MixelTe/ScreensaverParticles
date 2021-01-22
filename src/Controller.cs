using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverConections
{
	class Controller : IController
	{
		private readonly Settings _Settings;
		private readonly int _Height;
		private readonly int _Width;
		public readonly CPoint[] _CPoints;
		private readonly Random _Rnd = new Random(26);

		public Controller(int width, int height)
		{
			_Settings = Program.Settings;
			_Width = width;
			_Height = height;
			_CPoints = new CPoint[_Width * _Height / (500 * 500) * _Settings.Density];
			_CPoints = new CPoint[1];
			CreateAll();
		}
		private void CreateAll()
		{
			for (int i = 0; i < _CPoints.Length; i++)
			{
				_CPoints[i] = CreatePoint();
			}
		}
		public void Update()
		{
			for (int i = 0; i < _CPoints.Length; i++)
			{
				var el = _CPoints[i];
				if (el == null)
				{
					_CPoints[i] = CreatePoint();
					break;
				}
				else
				{
					el.Update();
				}
			}
		}

		private CPoint CreatePoint()
		{
			return new CPoint(_Rnd.Next(_Width), _Rnd.Next(_Height), _Width, _Height, _Rnd, GetColor(), _CPoints, _Settings);
		}
		private Color GetColor()
		{
			var h = _Rnd.Next(_Settings.ColorMin, _Settings.ColorMax);
			var l = _Rnd.Next((int)(_Settings.ColorLMin * 100), (int)(_Settings.ColorLMax * 100));

			return new HSL(h, 100, l).HSLToRGB().RGBToColor(255);
		}

		public void Dispose()
		{
			foreach (var p in _CPoints)
			{
				if (p != null) p.Dispose();
			}
		}
	}


	class CPoint: IDisposable
	{
		private readonly Settings _Settings;
		private double _X;
		private double _Y;
		private double _Speed;
		private double _Time = 0;
		private double _Counter = 0;
		private double _Acc = 0;
		private double _Direction;
		private double _RotateSpeed = 0;
		private readonly int _Width;
		private readonly int _Height;
		private readonly Random _Rnd;
		private CPoint[] _PointsConnected;
		private readonly CPoint[] _Points;
		private readonly SolidBrush _Brush;
		private readonly Pen _Pen;
		private HSL _dev_color;
		private readonly bool _dev_active = true;


		public CPoint(int x, int y, int width, int height, Random rnd, Color color, CPoint[] points, Settings settings)
		{
			_Settings = settings;
			_X = x;
			_Y = y;
			_Width = width;
			_Height = height;
			_Rnd = rnd;
			_Speed = _Settings.SpeedMax / 2;
			_Direction = rnd.Next(360) / 180d * Math.PI;
			_Brush = new SolidBrush(color);
			_Pen = new Pen(_Settings.ConnectionsColor)
			{
				Width = _Settings.ConnectionsWidth
			};
			_Points = points;
			_PointsConnected = new CPoint[points.Length];

			if (_dev_active)
			{
				var h = _Rnd.Next(_Settings.ColorMin, _Settings.ColorMax);
				var l = _Rnd.Next((int)(_Settings.ColorLMin * 100), (int)(_Settings.ColorLMax * 100));
				_dev_color = new HSL(h, 100, l);
			}
		}

		public void Update()
		{
			ChangeSpeed();
			Move();
			if (_Settings.DrawConections)
			{
				_PointsConnected = new CPoint[_Points.Length];
				for (int i = 0; i < _Points.Length; i++)
				{
					var p = _Points[i];
					if (p == this || p == null) continue;
					var connected = false;
					for (int o = 0; o < p._PointsConnected.Length; o++)
					{
						if (p._PointsConnected[o] == this)
						{
							connected = true;
							break;
						}
					}
					if (connected) continue;
					if (GetDistance(p._X, p._Y) <= Squared(_Settings.DistanceMax))
					{
						_PointsConnected[i] = p;
					}
				}
			}
		}
		private void Move()
		{
			_X += Math.Cos(_Direction) * _Speed;
			_Y += Math.Sin(_Direction) * _Speed;

			if (_X > _Width) _Direction = Math.PI - _Direction;
			if (_X < 0) _Direction = Math.PI - (_Direction - Math.PI) + Math.PI;
			if (_Y > _Height) _Direction = Math.PI - (_Direction + Math.PI / 2) - Math.PI / 2;
			if (_Y < 0) _Direction = Math.PI - (_Direction + Math.PI / 2) - Math.PI / 2;
			_X = Math.Max(Math.Min(_X, _Width), 0);
			_Y = Math.Max(Math.Min(_Y, _Height), 0);
		}
		private void ChangeSpeed()
		{
			_Speed += _Acc;
			_Speed = Math.Max(Math.Min(_Speed, _Settings.SpeedMax), -_Settings.SpeedMax);
			_Direction += _RotateSpeed;
			if (_Counter > _Time)
			{
				_Time = _Rnd.Next(_Settings.TimeMin, _Settings.TimeMax);
				_Counter = 0;
				var nextAcc = _Rnd.Next(0, (int)_Settings.SpeedMax) / 10d;
				if (_Speed == _Settings.SpeedMax) _Acc = -nextAcc;
				else if (_Speed == -_Settings.SpeedMax) _Acc = nextAcc;
				else
				{
					if (_Rnd.Next(2) == 1) nextAcc *= -1;
					_Acc = nextAcc;
				}
				_RotateSpeed = _Rnd.Next(0, (int)(_Settings.RotateSpeedMax * 360)) / 180d / Math.PI;
				if (_Rnd.Next(2) == 1) _RotateSpeed *= -1;
			}
			_Counter++;
		}
		private double GetDistance(double x, double y)
		{
			return Squared(x - _X) + Squared(y - _Y);
		}
		private double Squared(double num) => num * num;

		public void DrawConnections(Graphics g)
		{
			if (_Settings.DrawConections)
			{
				var P = new PointF((float)_X, (float)_Y);
				foreach (var p in _PointsConnected)
				{
					if (p != null)
					{
						var d = GetDistance(p._X, p._Y);
						d = Math.Min(d, Squared(_Settings.DistanceMax));
						d -= Squared(_Settings.DistanceShading);
						var A = 255;
						if (d > 0)
						{
							var a = d / (Squared(_Settings.DistanceMax) - Squared(_Settings.DistanceShading));
							A = (int)((1d - a) * 256);
						}
						A = (int)(A * _Settings.LineAlpha);
						_Pen.Color = Color.FromArgb(A, _Settings.ConnectionsColor);
						g.DrawLine(_Pen, P, new PointF((float)p._X, (float)p._Y));
					}
				}
			}
		}

		public void DrawPoint(Graphics g)
		{
			var b = _Brush;
			if (_dev_active)
			{
				b = new SolidBrush(_dev_color.HSLToRGB().RGBToColor(255));
				_dev_color = new HSL((_dev_color.H + 1) % 360, _dev_color.S, _dev_color.L);
			}
			g.FillEllipse(b, (float)(_X - _Settings.PointRadius / 2), (float)(_Y - _Settings.PointRadius / 2), _Settings.PointRadius, _Settings.PointRadius);
		}

		public void Dispose()
		{
			_Brush?.Dispose();
			_Pen?.Dispose();
		}
	}
}