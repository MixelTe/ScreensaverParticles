﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverConections
{
	enum ColorStyle
	{
		All,
		Red_Green,
		Red_Blue,
		Green_Red,
		Green_Blue,
		Blue_Red,
		Blue_Green,
	}
	class Controller : IController
	{
		private readonly ScreensaverSettings _Settings;
		private readonly int _Height;
		private readonly int _Width;
		public readonly CPoint[] _CPoints;
		private readonly Random _Rnd = new Random(1);

		private readonly int _Density;
		private readonly ColorStyle _ColorStyle = ColorStyle.Green_Blue;

		public Controller(int width, int height, ScreensaverSettings settings)
		{
			_Settings = settings;
			_Density = _Settings.Density;
			_Width = width;
			_Height = height;
			_CPoints = new CPoint[_Width * _Height / (500 * 500) * _Density];
		}
		public void Update()
		{
			for (int i = 0; i < _CPoints.Length; i++)
			{
				var el = _CPoints[i];
				if (el == null)
				{
					_CPoints[i] = new CPoint(_Rnd.Next(_Width), _Rnd.Next(_Height), _Width, _Height, _Rnd, GetColor(), _CPoints);
					break;
				}
				else
				{
					el.Update();
				}
			}
		}
		private Color GetColor()
		{
			int r, g, b;
			if (_ColorStyle == ColorStyle.Red_Blue)
			{
				r = _Rnd.Next(256);
				b = _Rnd.Next(r);
				g = _Rnd.Next(b);
			}
			else if (_ColorStyle == ColorStyle.Red_Green)
			{
				r = _Rnd.Next(256);
				g = _Rnd.Next(r);
				b = _Rnd.Next(g);
			}
			else if (_ColorStyle == ColorStyle.Green_Blue)
			{
				g = _Rnd.Next(256);
				b = _Rnd.Next(g);
				r = _Rnd.Next(b);
			}
			else if (_ColorStyle == ColorStyle.Green_Red)
			{
				g = _Rnd.Next(256);
				r = _Rnd.Next(g);
				b = _Rnd.Next(r);
			}
			else if (_ColorStyle == ColorStyle.Blue_Green)
			{
				b = _Rnd.Next(256);
				g = _Rnd.Next(b);
				r = _Rnd.Next(g);
			}
			else if (_ColorStyle == ColorStyle.Blue_Red)
			{
				b = _Rnd.Next(256);
				r = _Rnd.Next(b);
				g = _Rnd.Next(r);
			}
			else
			{
				r = _Rnd.Next(256);
				g = _Rnd.Next(256);
				b = _Rnd.Next(256);
			}
			return Color.FromArgb(r, g, b);
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
		private double _X;
		private double _Y;
		private double _Speed = _SpeedMax / 2;
		private double _Time = 0;
		private double _Counter = 0;
		private double _Acc = 0;
		private double _Direction;
		private double _RotateSpeed = 0;
		private readonly int _Width;
		private readonly int _Height;
		private readonly Random _Rnd;
		private CPoint[] _PointsConected;
		private readonly CPoint[] _Points;
		private readonly SolidBrush _Brush;

		private readonly bool _DrawConections = true;
		private readonly static double _SpeedMax = 10;
		private readonly double _RotateSpeedMax = 0.1;
		private readonly int _DistanceMax = 150;
		private readonly int _DistanceShading = 100;
		private readonly int _TimeMin = 5;
		private readonly int _TimeMax = 50;


		public CPoint(int x, int y, int width, int height, Random rnd, Color color, CPoint[] points)
		{
			_X = x;
			_Y = y;
			_Width = width;
			_Height = height;
			_Rnd = rnd;
			_Direction = new Random().Next(360) / 180d * Math.PI;
			_Brush = new SolidBrush(color);
			_Points = points;
			_PointsConected = new CPoint[points.Length];
		}

		public void Update()
		{
			ChangeSpeed();
			Move();
			if (_DrawConections)
			{
				_PointsConected = new CPoint[_Points.Length];
				for (int i = 0; i < _Points.Length; i++)
				{
					var p = _Points[i];
					if (p == this || p == null) continue;
					var conected = false;
					for (int o = 0; o < p._PointsConected.Length; o++)
					{
						if (p._PointsConected[o] == this)
						{
							conected = true;
							break;
						}
					}
					if (conected) continue;
					if (GetDistance(p._X, p._Y) <= Squared(_DistanceMax))
					{
						_PointsConected[i] = p;
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
			_Speed = Math.Max(Math.Min(_Speed, _SpeedMax), -_SpeedMax);
			_Direction += _RotateSpeed;
			if (_Counter > _Time)
			{
				_Time = _Rnd.Next(_TimeMin, _TimeMax);
				_Counter = 0;
				var nextAcc = _Rnd.Next(0, (int)_SpeedMax) / 10d;
				if (_Speed == _SpeedMax) _Acc = -nextAcc;
				else if (_Speed == -_SpeedMax) _Acc = nextAcc;
				else
				{
					if (_Rnd.Next(2) == 1) nextAcc *= -1;
					_Acc = nextAcc;
				}
				_RotateSpeed = _Rnd.Next(0, (int)(_RotateSpeedMax * 360)) / 180d / Math.PI;
				if (_Rnd.Next(2) == 1) _RotateSpeed *= -1;
			}
			_Counter++;
		}
		private double GetDistance(double x, double y)
		{
			return Squared(x - _X) + Squared(y - _Y);
		}
		private double Squared(double num) => num * num;

		public void Draw(Graphics g)
		{
			if (_DrawConections)
			{
				var P = new PointF((float)_X, (float)_Y);
				foreach (var p in _PointsConected)
				{
					if (p != null)
					{
						using (var pen = new Pen(Color.Blue))
						{
							var d = GetDistance(p._X, p._Y);
							d = Math.Min(d, Squared(_DistanceMax));
							d -= Squared(_DistanceShading);
							if (d > 0)
							{
								var a = d / (Squared(_DistanceMax) - Squared(_DistanceShading));
								pen.Color = Color.FromArgb((int)((1d - a) * 256), Color.Blue);
							}
							g.DrawLine(pen, P, new PointF((float)p._X, (float)p._Y));
						}
					}
				}
			}
			var r = 8;
			g.FillEllipse(_Brush, (float)(_X - r / 2), (float)(_Y - r / 2), r, r);
		}

		public void Dispose()
		{
			_Brush?.Dispose();
		}
	}
}