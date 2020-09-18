using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ScreenSaverConections
{
	class Controller : IController
	{
		private readonly ScreensaverSettings _Settings;
		private readonly int _Height;
		private readonly int _Width;
		public readonly CPoint[] _CPoints;
		private readonly Random _Rnd = new Random();

		private readonly int _Density;

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
					_CPoints[i] = new CPoint(_Rnd.Next(_Width), _Rnd.Next(_Height), _Width, _Height);
					break;
				}
				else
				{
					el.Update();
				}
			}
		}

		public void Dispose()
		{

		}
	}


	class CPoint
	{
		private double _X;
		private double _Y;
		private double _Speed = 5;
		private double _Time = 0;
		private double _Counter = 0;
		private double _Acc = 0;
		private double _Direction;
		private double _RotateSpeed = 0;
		private readonly int _Width;
		private readonly int _Height;

		private double _SpeedMax = 10;
		private double _RotateSpeedMax = 0.2;
		private int _TimeMin = 5;
		private int _TimeMax = 50;


		public CPoint(int x, int y, int width, int height)
		{
			_X = x;
			_Y = y;
			_Width = width;
			_Height = height;
			_Direction = new Random().Next(360) / 180d * Math.PI;
		}

		public void Update()
		{
			ChangeSpeed();
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
				var rnd = new Random();
				_Time = rnd.Next(_TimeMin, _TimeMax);
				_Counter = 0;
				var nextAcc = rnd.Next(0, (int)_SpeedMax) / 10d;
				if (_Speed == _SpeedMax) _Acc = -nextAcc;
				else if (_Speed == -_SpeedMax) _Acc = nextAcc;
				else
				{
					if (rnd.Next(2) == 1) nextAcc *= -1;
					_Acc = nextAcc;
				}
				_RotateSpeed = rnd.Next(0, (int)(_RotateSpeedMax * 360)) / 180d / Math.PI;
				if (rnd.Next(2) == 1) _RotateSpeed *= -1;
			}
			_Counter++;
		}

		public void Draw(Graphics g)
		{
			var r = 8;
			g.FillEllipse(Brushes.Aqua, (float)(_X - r / 2), (float)(_Y - r / 2), r, r);
		}
	}
}