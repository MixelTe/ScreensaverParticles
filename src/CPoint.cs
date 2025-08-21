using System;
using System.Drawing;
using System.Xml.Linq;

namespace ScreenSaverConections
{
	class CPoint : IDisposable
	{
		private readonly Settings _Settings;
		public readonly bool Bound;
		public readonly int Group;
		public bool Visible = true;
		public float Alpha = 1;
		private float _AlphaSpeed = 0.025f;
		private float _XStart;
		private float _YStart;
		public float X;
		public float Y;
		private float _Speed;
		private float _Time = 0;
		private float _Counter = 0;
		private float _Acc = 0;
		private float _Direction;
		private float _RotateSpeed = 0;
		public Color _Color;
		private readonly int _Width;
		private readonly int _Height;
		private readonly Random _Rnd;

		private readonly int _MaxDistanceWhenBound = 10;


		public CPoint(int x, int y, int width, int height, Random rnd, Color color, Settings settings, bool bound, int group = 0)
		{
			_Settings = settings;
			Bound = bound;
			Group = group;
			_XStart = x;
			_YStart = y;
			X = x;
			Y = y;
			_Width = width;
			_Height = height;
			_Rnd = rnd;
			_Speed = _Settings.SpeedMax / 2;
			_Direction = (float)(rnd.Next(360) / 180d * Math.PI);
			_Color = color;
		}

		public void Update()
		{
			ChangeSpeed();
			Move();
			if (Visible) Alpha = Math.Min(Alpha + _AlphaSpeed, 1);
			else Alpha = Math.Max(Alpha - _AlphaSpeed, 0);
		}
		private void Move()
		{
			X += (float)(Math.Cos(_Direction) * _Speed);
			Y += (float)(Math.Sin(_Direction) * _Speed);

			if (X > _Width) _Direction = (float)(Math.PI - _Direction);
			if (X < 0) _Direction = (float)(Math.PI - (_Direction - Math.PI) + Math.PI);
			if (Y > _Height) _Direction = (float)(Math.PI - (_Direction + Math.PI / 2) - Math.PI / 2);
			if (Y < 0) _Direction = (float)(Math.PI - (_Direction + Math.PI / 2) - Math.PI / 2);
			X = Math.Max(Math.Min(X, _Width), 0);
			Y = Math.Max(Math.Min(Y, _Height), 0);

			if (Bound)
			{
				var speed = _Settings.DEV_Presentation_BoundSpeed / Program.SizeMul;
				var speedX = speed * (_XStart - X) / _MaxDistanceWhenBound;
				var speedY = speed * (_YStart - Y) / _MaxDistanceWhenBound;
				X += speedX;
				Y += speedY;
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
				var nextAcc = _Rnd.Next(0, (int)_Settings.SpeedMax) / 10f;
				if (_Speed == _Settings.SpeedMax) _Acc = -nextAcc;
				else if (_Speed == -_Settings.SpeedMax) _Acc = nextAcc;
				else
				{
					if (_Rnd.Next(2) == 1) nextAcc *= -1;
					_Acc = nextAcc;
				}
				_RotateSpeed = (float)(_Rnd.Next(0, (int)(_Settings.RotateSpeedMax * 360)) / 180d / Math.PI);
				if (_Rnd.Next(2) == 1) _RotateSpeed *= -1;
			}
			_Counter++;
		}

		public void Draw(IGraphics g)
		{
			if (Alpha == 0) return;
			var c = _Color;
			if (Alpha != 1) c = Color.FromArgb((int)(Alpha * 255), c);
			g.FillEllipse(c, X, Y, _Settings.PointRadius, _Settings.PointRadius);
		}

		public void Dispose()
		{
		}

		internal void SetStartPos(int x, int y)
		{
			_XStart = x;
			_YStart = y;
		}
	}
}
