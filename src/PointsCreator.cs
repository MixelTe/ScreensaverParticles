using System;
using System.Drawing;

namespace ScreenSaverConections
{
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
			_Space = (int)(_Settings.DistanceMax * Program.SizeMul);
			var clockMaxWidth = (int)Math.Round(width * _Settings.ClockSize - _Space * 3);
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
				var st1 = int.Parse(_Settings.DEV_ClockFastMode_StartTime.Substring(0, 2));
				var st2 = int.Parse(_Settings.DEV_ClockFastMode_StartTime.Substring(2, 2));
				var date = new DateTime(1, 1, 1, st1, st2, 0);
				now = date.AddMinutes(_Settings.DEV_counter / _Settings.DEV_ClockFastMode_TicksForChange);
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
			var sShift = 0;
			var addPoints = pointsCount - pointsForRect * rects.Length;
			var addPointsVisible = pointsCount % 7;
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
				var visibleCount = pointsForRectVisible + (i < addPointsVisible ? 1 : 0);
				CreatePoints(rectInt, s, e, points, pos, newP, visibleCount);
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
