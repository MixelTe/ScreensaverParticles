﻿using System;
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
		private readonly int _OneNumRange;
		private readonly Random _Rnd = new Random(1);
		private readonly PointsCreator _NumsCreator;

		public Controller(int width, int height)
		{
			_Settings = Program.Settings;
			_Width = width;
			_Height = height;
			_OneNumRange = (_Width * _Height / 2) / (500 * 500) * _Settings.Density;
			if (_Settings.ClockMode) _CPoints = new CPoint[_OneNumRange * 4];
			else _CPoints = new CPoint[_Width * _Height / (500 * 500) * _Settings.Density];
			_NumsCreator = new PointsCreator(width, height, _Settings, _Rnd);
			CreateAll();
		}
		private void CreateAll()
		{
			if (_Settings.ClockMode)
			{
				var time = _NumsCreator.GetCurrentTime();
				_NumsCreator.CreateNum(time[0], _OneNumRange * 0, _OneNumRange * 1, _CPoints, 0);
				_NumsCreator.CreateNum(time[1], _OneNumRange * 1, _OneNumRange * 2, _CPoints, 1);
				_NumsCreator.CreateNum(time[2], _OneNumRange * 2, _OneNumRange * 3, _CPoints, 2);
				_NumsCreator.CreateNum(time[3], _OneNumRange * 3, _OneNumRange * 4, _CPoints, 3);
			}
			else
			{
				_NumsCreator.CreatePointsSimple(_CPoints);
			}
		}
		void IController.Update()
		{
			for (int i = 0; i < _CPoints.Length; i++)
			{
				var el = _CPoints[i];
				if (el != null)
				{
					el.Update();
				}
			}
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
		private readonly double _XStart;
		private readonly double _YStart;
		private readonly bool _Bound;
		private readonly int _Group;
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

		private readonly int _MaxDistanceWhenBound = 10;


		public CPoint(int x, int y, int width, int height, Random rnd, Color color, CPoint[] points, Settings settings, bool bound, int group = 0)
		{
			_Settings = settings;
			_Bound = bound;
			_Group = group;
			_XStart = x;
			_YStart = y;
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
					if (_Bound && _Group != p._Group) continue;
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

			if (_Bound)
			{
				var speed = 1;
				var speedX = speed * (_XStart - _X) / _MaxDistanceWhenBound;
				var speedY = speed * (_YStart - _Y) / _MaxDistanceWhenBound;
				_X += speedX;
				_Y += speedY;
			}
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
			g.FillEllipse(_Brush, (float)(_X - _Settings.PointRadius / 2), (float)(_Y - _Settings.PointRadius / 2), _Settings.PointRadius, _Settings.PointRadius);
		}

		public void Dispose()
		{
			_Brush?.Dispose();
			_Pen?.Dispose();
		}
	}


	class PointsCreator
	{
		private readonly Settings _Settings;
		private readonly int _WidthScr;
		private readonly int _HeightScr;
		private readonly int _X;
		private readonly int _Y;
		private readonly int _Width;
		private readonly int _Height;
		private readonly int _Space;
		private readonly Random _Rnd;

		public PointsCreator(int width, int height, Settings settings, Random rnd)
		{
			_Settings = settings;
			_Rnd = rnd;
			_WidthScr = width;
			_HeightScr = height;
			var ClockMaxWidth = (int)Math.Round(width * _Settings.ClockSize);
			var ClockMaxHeight = (int)Math.Round(height * _Settings.ClockSize);
			int ClockWidth, ClockHeight;
			if (ClockMaxHeight * 2 > ClockMaxWidth)
			{
				ClockWidth = ClockMaxWidth;
				ClockHeight = ClockMaxWidth / 2;
			}
			else
			{
				ClockWidth = ClockMaxHeight * 2;
				ClockHeight = ClockMaxHeight;
			}
			_Width = ClockWidth / 4;
			_Height = ClockHeight;
			_Space = _Width / 2;

			_X = (width - (ClockWidth + _Space * 3)) / 2;
			_Y = (height - ClockHeight) / 2;
		}

		public void CreatePointsSimple(CPoint[] points)
		{
			var rect = new Rectangle(0, 0, _WidthScr, _HeightScr);
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = CreatePoint(rect, points, false);
			}
		}
		private CPoint CreatePoint(Rectangle rect, CPoint[] points, bool bound, int group = 0)
		{
			return new CPoint(_Rnd.Next(rect.Width) + rect.X, _Rnd.Next(rect.Height) + rect.Y, _WidthScr, _HeightScr, _Rnd, GetColor(), points, _Settings, bound, group);
		}
		private Color GetColor()
		{
			var h = _Rnd.Next(_Settings.ColorMin, _Settings.ColorMax);
			var l = _Rnd.Next((int)(_Settings.ColorLMin * 100), (int)(_Settings.ColorLMax * 100));

			return new HSL(h, 100, l).HSLToRGB().RGBToColor(255);
		}

		public int[] GetCurrentTime()
		{
			var hours = DateTime.Now.Hour.ToString();
			var minute = DateTime.Now.Minute.ToString();
			int n1, n2, n3, n4;
			if (hours.Length == 1)
			{
				n1 = 0;
				n2 = int.Parse(hours);
			}
			else
			{
				n1 = int.Parse(hours.Substring(0, 1));
				n2 = int.Parse(hours.Substring(1, 1));
			}
			if (minute.Length == 1)
			{
				n3 = 0;
				n4 = int.Parse(minute);
			}
			else
			{
				n3 = int.Parse(minute.Substring(0, 1));
				n4 = int.Parse(minute.Substring(1, 1));
			}
			return new int[] { n1, n2, n3, n4 };
		}
		public void CreateNum(int num, int rs, int re, CPoint[] points, int pos)
		{
			switch (num)
			{
				case 0: CreateNum_8(rs, re, points, pos); break;
				case 1: CreateNum_8(rs, re, points, pos); break;
				case 2: CreateNum_8(rs, re, points, pos); break;
				case 3: CreateNum_8(rs, re, points, pos); break;
				case 4: CreateNum_8(rs, re, points, pos); break;
				case 5: CreateNum_8(rs, re, points, pos); break;
				case 6: CreateNum_8(rs, re, points, pos); break;
				case 7: CreateNum_8(rs, re, points, pos); break;
				case 8: CreateNum_8(rs, re, points, pos); break;
				case 9: CreateNum_8(rs, re, points, pos); break;
			}
		}

		private void CreateNum_8(int rs, int re, CPoint[] points, int pos)
		{
			var rects = new RectangleF[] {
				new RectangleF(0.25f, 0, 0.5f, 0.142f), //¯
				new RectangleF(0.25f, 0.426f, 0.5f, 0.142f), //-
				new RectangleF(0.25f, 0.858f, 0.5f, 0.142f), //_
				new RectangleF(0, 0, 0.142f, 0.568f), //|
				new RectangleF(0, 0.426f, 0.142f, 0.568f), //|
				new RectangleF(0.858f, 0, 0.142f, 0.568f), // |
				new RectangleF(0.858f, 0.426f, 0.142f, 0.568f), // |
			};
			CreateNum_Rects(rects, rs, re, points, pos);
		}
		private void CreateNum_Rects(RectangleF[] rects, int rs, int re, CPoint[] points, int pos)
		{
			var pointsCount = re - rs;
			var xShift = (_Width + _Space) * pos + _X;
			var pointsForRect = pointsCount / rects.Length;
			for (int i = 0; i < rects.Length; i++)
			{
				var rect = rects[i];
				rect = new RectangleF(rect.X * _Width, rect.Y * _Height, rect.Width * _Width, rect.Height * _Height);
				var rectInt = new Rectangle((int)rect.X + xShift, (int)rect.Y + _Y, (int)rect.Width, (int)rect.Height);
				var s = rs + pointsForRect * i;
				var e = rs + pointsForRect * (i + 1);
				CreatePoints(rectInt, s, e, points, pos);
			}
		}
		private void CreatePoints(Rectangle rect, int rs, int re, CPoint[] points, int pos)
		{
			Program.rectangles.Add(rect);
			for (int i = rs; i < re; i++)
			{
				points[i] = CreatePoint(rect, points, true, pos);
			}
		}
	}
}
