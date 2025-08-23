namespace ScreenSaverParticles;

class PointsCreator
{
	private readonly Settings _settings;
	private readonly int _widthScr;
	private readonly int _heightScr;
	private readonly int _x;
	private readonly int _y;
	private readonly int _width;
	private readonly int _height;
	private readonly int _space;

	public PointsCreator(int width, int height, Settings settings)
	{
		_settings = settings;
		_widthScr = width;
		_heightScr = height;
		_space = (int)(_settings.DistanceMax * Program.SizeMul);
		var clockMaxWidth = (int)Math.Round(width * _settings.ClockSize - _space * 3);
		var clockMaxHeight = (int)Math.Round(height * _settings.ClockSize);
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
		_width = clockWidth / 4;
		_height = clockHeight;

		_x = (width - (clockWidth + _space * 3)) / 2;
		_y = (height - clockHeight) / 2;
	}

	public void CreatePointsSimple(CPoint[] points)
	{
		var rect = new Rectangle(0, 0, _widthScr, _heightScr);
		for (int i = 0; i < points.Length; i++)
			points[i] = CreatePoint(rect, false);
	}
	private CPoint CreatePoint(Rectangle rect, bool bound, int group = 0)
	{
		//return new CPoint(rect.X, rect.Y, _widthScr, _heightScr, GetColor(), _settings, bound, group);
		return new CPoint(Random.Shared.Next(rect.Width) + rect.X, Random.Shared.Next(rect.Height) + rect.Y, _widthScr, _heightScr, GetColor(), _settings, bound, group);
	}
	private CPoint CreatePoint(Point point, bool bound, int group = 0)
	{
		return new CPoint(point.X, point.Y, _widthScr, _heightScr, GetColor(), _settings, bound, group);
	}
	private Color GetColor()
	{
		var h = Random.Shared.Next(_settings.ColorMin, _settings.ColorMax);
		var l = Random.Shared.Next((int)(_settings.ColorLMin * 100), (int)(_settings.ColorLMax * 100));

		return new HSL(h, 100, l).HSLToRGB().RGBToColor(255);
	}

	public int[] GetCurrentTime(out string time)
	{
		var now = DateTime.Now;
		if (_settings.DEV_ClockFakeTimeMode)
		{
			var st1 = int.Parse(_settings.DEV_ClockFastMode_StartTime[..2]);
			var st2 = int.Parse(_settings.DEV_ClockFastMode_StartTime[2..2]);
			var date = new DateTime(1, 1, 1, st1, st2, 0);
			now = date.AddMinutes(_settings.DEV_counter / _settings.DEV_ClockFastMode_TicksForChange);
		}
		var n1 = now.Hour / 10;
		var n2 = now.Hour % 10;
		var n3 = now.Minute / 10;
		var n4 = now.Minute % 10;
		time = $"{n1}{n2}{n3}{n4}";
		return [n1, n2, n3, n4];
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
		var r = _space / 5;
		var d = (int)Math.Round(_height * 0.3);
		var xShift = _width * 2 + _space + _x + (_space - r) / 2;
		var yShift = _y + (_height - d) / 2 + d * pos;
		var step = Math.PI * 2 / pointsCount;
		for (int i = 0; i < pointsCount; i++)
		{
			var x = (int)Math.Round(Math.Cos(step * i) * r + xShift);
			var y = (int)Math.Round(Math.Sin(step * i) * r + yShift);
			var point = new Point(x, y);
			points[rs + i] = CreatePoint(point, true, pos + 4);
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
	private static RectangleF GetRectangle(NumPart part)
	{
		return part switch
		{
			NumPart.Top => new RectangleF(0.125f, 0, 0.75f, 0.142f),
			NumPart.Middle => new RectangleF(0.125f, 0.426f, 0.75f, 0.142f),
			NumPart.Bottom => new RectangleF(0.125f, 0.858f, 0.75f, 0.142f),
			NumPart.LeftTop => new RectangleF(0, 0.071f, 0.25f, 0.426f),
			NumPart.LeftBottom => new RectangleF(0, 0.497f, 0.25f, 0.426f),
			NumPart.RightTop => new RectangleF(0.75f, 0.071f, 0.25f, 0.426f),
			NumPart.RightBottom => new RectangleF(0.75f, 0.497f, 0.25f, 0.426f),
			_ => new RectangleF(),
		};
	}
	private void CreateNum_Rects(RectangleF[] rects, int rs, int re, CPoint[] points, int pos, bool newP)
	{
		var pointsCount = re - rs;
		var xShift = (_width + _space) * pos + _x;
		var pointsForRect = pointsCount / rects.Length;
		var pointsForRectVisible = pointsCount / 7;
		var sShift = 0;
		var addPoints = pointsCount - pointsForRect * rects.Length;
		var addPointsVisible = pointsCount % 7;
		for (int i = 0; i < rects.Length; i++)
		{
			var rect = rects[i];
			rect = new RectangleF(rect.X * _width, rect.Y * _height, rect.Width * _width, rect.Height * _height);
			var rectInt = new Rectangle((int)rect.X + xShift, (int)rect.Y + _y, (int)rect.Width, (int)rect.Height);
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
			xPoints[i] = rect.X + (int)(i * width) % rect.Width;
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
				points[i] = CreatePoint(pRect, true, pos);
				if (pi > visibleCount) points[i].Alpha = 0;
			}
			else
			{
				var x = Random.Shared.Next(pRect.Width) + pRect.X;
				var y = Random.Shared.Next(pRect.Height) + pRect.Y;
				points[i]?.SetStartPos(x, y);
			}
			if (points[i] != null)
				points[i].Visible = pi <= visibleCount;
		}
	}

	public void ReCreateNum(int num, int rs, int re, CPoint[] points, int pos)
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
