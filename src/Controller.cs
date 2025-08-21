using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

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
		private readonly PointsCreator _PointsCreator;
		private string _PastTime;

		public Controller(int width, int height)
		{
			_Settings = Program.Settings;
			_Width = width;
			_Height = height;
			_OneNumRange = _Width * _Height / (500 * 500) * _Settings.Density;
			if (_Settings.ClockMode) _CPoints = new CPoint[_OneNumRange * 4 + _Settings.DEV_PointsPerDot * 2];
			else _CPoints = new CPoint[_Width * _Height / (500 * 500) * _Settings.Density];
			_PointsCreator = new PointsCreator(width, height, _Settings, _Rnd);
			CreateAll();
		}
		private void CreateAll()
		{
			if (_Settings.ClockMode)
			{
				var time = _PointsCreator.GetCurrentTime(out _PastTime);
				//time = new int[] { 7, 8, 8, 8 };
				_PointsCreator.CreateNum(time[0], _OneNumRange * 0, _OneNumRange * 1, _CPoints, 0);
				_PointsCreator.CreateNum(time[1], _OneNumRange * 1, _OneNumRange * 2, _CPoints, 1);
				_PointsCreator.CreateNum(time[2], _OneNumRange * 2, _OneNumRange * 3, _CPoints, 2);
				_PointsCreator.CreateNum(time[3], _OneNumRange * 3, _OneNumRange * 4, _CPoints, 3);
				_PointsCreator.CreateDot(_OneNumRange * 4 + _Settings.DEV_PointsPerDot * 0, _OneNumRange * 4 + _Settings.DEV_PointsPerDot * 1, _CPoints, 0);
				_PointsCreator.CreateDot(_OneNumRange * 4 + _Settings.DEV_PointsPerDot * 1, _OneNumRange * 4 + _Settings.DEV_PointsPerDot * 2, _CPoints, 1);
			}
			else
			{
				_PointsCreator.CreatePointsSimple(_CPoints);
			}
		}
		void IController.Update()
		{
			if (_Settings.ClockMode)
			{
				var time = _PointsCreator.GetCurrentTime(out var sTime);
				if (_PastTime != sTime)
				{
					if (_PastTime[0] != sTime[0]) _PointsCreator.ReCreateNum(time[0], _OneNumRange * 0, _OneNumRange * 1, _CPoints, 0);
					if (_PastTime[1] != sTime[1]) _PointsCreator.ReCreateNum(time[1], _OneNumRange * 1, _OneNumRange * 2, _CPoints, 1);
					if (_PastTime[2] != sTime[2]) _PointsCreator.ReCreateNum(time[2], _OneNumRange * 2, _OneNumRange * 3, _CPoints, 2);
					if (_PastTime[3] != sTime[3]) _PointsCreator.ReCreateNum(time[3], _OneNumRange * 3, _OneNumRange * 4, _CPoints, 3);
				}
				_PastTime = sTime;
			}

			foreach (var el in _CPoints)
			{
				el?.Update();
			}
		}
		public void Dispose()
		{
			foreach (var p in _CPoints)
			{
				p?.Dispose();
			}
		}

		internal void DrawConnections(IGraphics g)
		{
			var dMax = Squared(_Settings.DistanceMax);
			var dShade = Squared(_Settings.DistanceShading);
			for (int i = 0; i < _CPoints.Length; i++)
			{
				var p1 = _CPoints[i];
				if (p1 == null || p1.Alpha == 0) continue;
				for (int j = i + 1; j < _CPoints.Length; j++)
				{
					var p2 = _CPoints[j];
					if (p2 == null || p2.Alpha == 0) continue;

					if (p1.Bound && p1.Group != p2.Group) break;  // rely on that points are sorted by group

					var d = Squared(p1.X - p2.X) + Squared(p1.Y - p2.Y);
					if (d > dMax) continue;

					d -= dShade;
					var A = 255;
					if (d > 0)
					{
						var a = d / (dMax - dShade);
						A = (int)((1f - a) * 255);
					}
					A = (int)(A * _Settings.LineAlpha);
					A = (int)(A * Math.Min(p1.Alpha, p2.Alpha));
					var color = Color.FromArgb(A, _Settings.ConnectionsColor);
					g.DrawLine(color, _Settings.ConnectionsWidth, p1.X, p1.Y, p2.X, p2.Y);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float Squared(float num) => num * num;
	}


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
		private readonly Color _Color;
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
				var speed = 1;
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

		private int DEV_timeCounter = 0;
		private int DEV_timeAdd = 0;

		public PointsCreator(int width, int height, Settings settings, Random rnd)
		{
			_Settings = settings;
			_Rnd = rnd;
			_WidthScr = width;
			_HeightScr = height;
			var clockMaxWidth = (int)Math.Round(width * _Settings.ClockSize) - _Settings.DistanceMax * 3;
			var clockMaxHeight = (int)Math.Round(height * _Settings.ClockSize);
			int clockWidth, clockHeight;
			if (clockMaxHeight * 2 > clockMaxWidth)
			{
				clockWidth = clockMaxWidth;
				clockHeight = clockMaxWidth / 2;
			}
			else
			{
				clockWidth = clockMaxHeight * 2;
				clockHeight = clockMaxHeight;
			}
			_Space = _Settings.DistanceMax;
			_Width = clockWidth / 4;
			_Height = clockHeight;

			_X = (width - (clockWidth + _Space * 3)) / 2;
			_Y = (height - clockHeight) / 2;
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
			//return new CPoint(rect.X, rect.Y, _WidthScr, _HeightScr, _Rnd, GetColor(), _Settings, bound, group);
			return new CPoint(_Rnd.Next(rect.Width) + rect.X, _Rnd.Next(rect.Height) + rect.Y, _WidthScr, _HeightScr, _Rnd, GetColor(), _Settings, bound, group);
		}
		private CPoint CreatePoint(Point point, CPoint[] points, bool bound, int group = 0)
		{
			return new CPoint(point.X, point.Y, _WidthScr, _HeightScr, _Rnd, GetColor(), _Settings, bound, group);
		}
		private Color GetColor()
		{
			var h = _Rnd.Next(_Settings.ColorMin, _Settings.ColorMax);
			var l = _Rnd.Next((int)(_Settings.ColorLMin * 100), (int)(_Settings.ColorLMax * 100));

			return new HSL(h, 100, l).HSLToRGB().RGBToColor(255);
		}

		public int[] GetCurrentTime(out string time)
		{
			var now = DateTime.Now;
			if (_Settings.DEV_ClockFakeTimeMode)
			{
				DEV_timeCounter += 1;
				if (DEV_timeCounter >= _Settings.DEV_ClockFastMode_TicksForChange)
				{
					DEV_timeAdd += 1;
					DEV_timeCounter = 0;
				}
				var st1 = int.Parse(_Settings.DEV_ClockFastMode_StartTime.Substring(0, 2));
				var st2 = int.Parse(_Settings.DEV_ClockFastMode_StartTime.Substring(2, 2));
				var date = new DateTime(1, 1, 1, st1, st2, 0);
				now = date.AddMinutes(DEV_timeAdd);
			}
			var n1 = now.Hour / 10;
			var n2 = now.Hour % 10;
			var n3 = now.Minute / 10;
			var n4 = now.Minute % 10;
			time = $"{n1}{n2}{n3}{n4}";
			return new int[] { n1, n2, n3, n4 };
		}

		public void CreateNum(int num, int rs, int re, CPoint[] points, int pos)
		{
			switch (num)
			{
				case 0: CreateNum_0(rs, re, points, pos, true); break;
				case 1: CreateNum_1(rs, re, points, pos, true); break;
				case 2: CreateNum_2(rs, re, points, pos, true); break;
				case 3: CreateNum_3(rs, re, points, pos, true); break;
				case 4: CreateNum_4(rs, re, points, pos, true); break;
				case 5: CreateNum_5(rs, re, points, pos, true); break;
				case 6: CreateNum_6(rs, re, points, pos, true); break;
				case 7: CreateNum_7(rs, re, points, pos, true); break;
				case 8: CreateNum_8(rs, re, points, pos, true); break;
				case 9: CreateNum_9(rs, re, points, pos, true); break;
			}
		}
		public void CreateDot(int rs, int re, CPoint[] points, int pos)
		{
			var pointsCount = re - rs;
			var r = _Space / 5;
			var d = (int)Math.Round(_Height * 0.3);
			var xShift = _Width * 2 + _Space + _X + (_Space - r) / 2;
			var yShift = _Y + (_Height - d) / 2 + d * pos;
			var step = Math.PI * 2 / pointsCount;
			for (int i = 0; i < pointsCount; i++)
			{
				var x = (int)Math.Round(Math.Cos(step * i) * r + xShift);
				var y = (int)Math.Round(Math.Sin(step * i) * r + yShift);
				var point = new Point(x, y);
				points[rs + i] = CreatePoint(point, points, true, pos + 4);
			}

		}

		private void CreateNum_0(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.LeftBottom),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_1(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_2(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftBottom),
				GetRectangle(NumPart.RightTop),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_3(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_4(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_5(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_6(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.LeftBottom),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_7(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_8(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.LeftBottom),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private void CreateNum_9(int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var rects = new RectangleF[] {
				GetRectangle(NumPart.Top),
				GetRectangle(NumPart.Middle),
				GetRectangle(NumPart.Bottom),
				GetRectangle(NumPart.LeftTop),
				GetRectangle(NumPart.RightTop),
				GetRectangle(NumPart.RightBottom),
			};
			CreateNum_Rects(rects, rs, re, points, pos, newP);
		}
		private RectangleF GetRectangle(NumPart part)
		{
			switch (part)
			{
				case NumPart.Top: return new RectangleF(0.125f, 0, 0.75f, 0.142f);
				case NumPart.Middle: return new RectangleF(0.125f, 0.426f, 0.75f, 0.142f);
				case NumPart.Bottom: return new RectangleF(0.125f, 0.858f, 0.75f, 0.142f);
				case NumPart.LeftTop: return new RectangleF(0, 0.071f, 0.25f, 0.426f);
				case NumPart.LeftBottom: return new RectangleF(0, 0.497f, 0.25f, 0.426f);
				case NumPart.RightTop: return new RectangleF(0.75f, 0.071f, 0.25f, 0.426f);
				case NumPart.RightBottom: return new RectangleF(0.75f, 0.497f, 0.25f, 0.426f);
			}
			return new RectangleF();
		}
		private void CreateNum_Rects(RectangleF[] rects, int rs, int re, CPoint[] points, int pos, bool newP)
		{
			var pointsCount = re - rs;
			var xShift = (_Width + _Space) * pos + _X;
			var pointsForRect = pointsCount / rects.Length;
			var pointsForRectVisible = pointsCount / 7;
			var visibleCount = pointsCount / 7;
			var sShift = 0;
			var addPoints = pointsCount - pointsForRect * rects.Length;
			for (int i = 0; i < rects.Length; i++)
			{
				var rect = rects[i];
				rect = new RectangleF(rect.X * _Width, rect.Y * _Height, rect.Width * _Width, rect.Height * _Height);
				var rectInt = new Rectangle((int)rect.X + xShift, (int)rect.Y + _Y, (int)rect.Width, (int)rect.Height);
				var s = rs + pointsForRect * i + sShift;
				var e = rs + pointsForRect * (i + 1) + sShift;
				if (addPoints > 0)
				{
					e += 1;
					sShift += 1;
					addPoints -= 1;
				}
				CreatePoints(rectInt, s, e, points, pos, newP, pointsForRectVisible);
			}
		}
		private void CreatePoints(Rectangle rect, int rs, int re, CPoint[] points, int pos, bool newP, int visibleCount)
		{
			//Program.rectangles.Add(rect);
			var pointsCount = re - rs;
			var width = rect.Width / (float)visibleCount;
			var height = rect.Height / (float)visibleCount;
			var xPoints = new int[pointsCount];
			for (int i = 0; i < xPoints.Length; i++)
			{
				xPoints[i] = rect.X + (int)(i * width) % rect.Width;
			}
			xPoints.Shuffle();
			for (int i = rs; i < re; i++)
			{
				var pi = i - rs;
				//var pRect = new Rectangle(rect.X + (int)(pi * width) % rect.Width, rect.Y, (int)width, rect.Height);
				//var pRect = new Rectangle(rect.X + (int)(pi * width) % rect.Width, rect.Y + (int)(pi * height) % rect.Height, (int)width, (int)height);
				var pRect = new Rectangle(xPoints[pi], rect.Y + (int)(pi * height) % rect.Height, (int)width, (int)height);
				//Program.rectangles.Add(pRect);
				//pRect = rect;
				if (newP)
				{
					points[i] = CreatePoint(pRect, points, true, pos);
					if (pi > visibleCount) points[i].Alpha = 0;
				}
				else
				{
					var x = _Rnd.Next(pRect.Width) + pRect.X;
					var y = _Rnd.Next(pRect.Height) + pRect.Y;
					points[i]?.SetStartPos(x, y);
				}
				if (points[i] != null)
					points[i].Visible = pi <= visibleCount;
			}
		}

		internal void ReCreateNum(int num, int rs, int re, CPoint[] points, int pos)
		{
			switch (num)
			{
				case 0: CreateNum_0(rs, re, points, pos, false); break;
				case 1: CreateNum_1(rs, re, points, pos, false); break;
				case 2: CreateNum_2(rs, re, points, pos, false); break;
				case 3: CreateNum_3(rs, re, points, pos, false); break;
				case 4: CreateNum_4(rs, re, points, pos, false); break;
				case 5: CreateNum_5(rs, re, points, pos, false); break;
				case 6: CreateNum_6(rs, re, points, pos, false); break;
				case 7: CreateNum_7(rs, re, points, pos, false); break;
				case 8: CreateNum_8(rs, re, points, pos, false); break;
				case 9: CreateNum_9(rs, re, points, pos, false); break;
			}
		}
	}
	enum NumPart
	{
		Top,
		Middle,
		Bottom,
		LeftTop,
		LeftBottom,
		RightTop,
		RightBottom,
	}
}
