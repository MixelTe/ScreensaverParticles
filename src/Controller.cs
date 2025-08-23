using System.Runtime.CompilerServices;

namespace ScreenSaverParticles;

class Controller : IController
{
	private readonly Settings _settings;
	private readonly int _height;
	private readonly int _width;
	public readonly CPoint[] Points;
	private readonly int _oneNumRange;
	private readonly PointsCreator _pointsCreator;
	private string _pastTime = "";
	private Point[] _DEV_poses1 = [];
	private Point[] _DEV_poses2 = [];
	private Color[] _DEV_colors = [];

	public Controller(int width, int height)
	{
		_settings = Program.Settings;
		_width = width;
		_height = height;
		_oneNumRange = (int)(_width * _height / (500f * 500) * _settings.Density / Program.SizeMul);
		if (_settings.ClockMode) Points = new CPoint[_oneNumRange * 4 + _settings.DEV_PointsPerDot * 2];
		else Points = new CPoint[(int)(_width * _height / (500f * 500) * _settings.Density)];
		_pointsCreator = new PointsCreator(width, height, _settings);
		CreateAll();
	}
	private void CreateAll()
	{
		if (_settings.ClockMode)
		{
			var time = _pointsCreator.GetCurrentTime(out _pastTime);
			//time = new int[] { 7, 8, 8, 8 };
			_pointsCreator.CreateNum(time[0], _oneNumRange * 0, _oneNumRange * 1, Points, 0);
			_pointsCreator.CreateNum(time[1], _oneNumRange * 1, _oneNumRange * 2, Points, 1);
			_pointsCreator.CreateNum(time[2], _oneNumRange * 2, _oneNumRange * 3, Points, 2);
			_pointsCreator.CreateNum(time[3], _oneNumRange * 3, _oneNumRange * 4, Points, 3);
			_pointsCreator.CreateDot(_oneNumRange * 4 + _settings.DEV_PointsPerDot * 0, _oneNumRange * 4 + _settings.DEV_PointsPerDot * 1, Points, 0);
			_pointsCreator.CreateDot(_oneNumRange * 4 + _settings.DEV_PointsPerDot * 1, _oneNumRange * 4 + _settings.DEV_PointsPerDot * 2, Points, 1);
		}
		else
		{
			_pointsCreator.CreatePointsSimple(Points);
		}
	}
	void IController.Update()
	{
		if (_settings.ClockMode)
		{
			var time = _pointsCreator.GetCurrentTime(out var sTime);
			if (_pastTime != sTime)
			{
				if (_pastTime[0] != sTime[0]) _pointsCreator.ReCreateNum(time[0], _oneNumRange * 0, _oneNumRange * 1, Points, 0);
				if (_pastTime[1] != sTime[1]) _pointsCreator.ReCreateNum(time[1], _oneNumRange * 1, _oneNumRange * 2, Points, 1);
				if (_pastTime[2] != sTime[2]) _pointsCreator.ReCreateNum(time[2], _oneNumRange * 2, _oneNumRange * 3, Points, 2);
				if (_pastTime[3] != sTime[3]) _pointsCreator.ReCreateNum(time[3], _oneNumRange * 3, _oneNumRange * 4, Points, 3);
			}
			_pastTime = sTime;
		}

		foreach (var el in Points) 
			el?.Update();

		if (_settings.DEV_Presentation)
		{
			if (_settings.DEV_counter == 1)
			{
				_settings.DEV_ClockFastMode_TicksForChange = 300;
				_settings.DEV_Presentation_BoundSpeed = 0;
				_settings.DEV_Presentation_VisibleI = (int)(Points.Length * 0.5f);
				_settings.DEV_Presentation_VisibleAlpha = 0;
				_settings.PointRadius = 5;
				_settings.ConnectionsWidth = 3;
				_settings.DistanceMax = 200;
				_DEV_poses1 = new Point[Points.Length];
				_DEV_poses2 = new Point[Points.Length];
				_DEV_colors = new Color[Points.Length];
				for (int i = 0; i < Points.Length; i++)
				{
					var p = Points[i];
					var pos = new Point(Random.Shared.Next(_width), Random.Shared.Next(_height));
					_DEV_poses1[i] = new Point((int)p.X, (int)p.Y);
					_DEV_poses2[i] = pos;
					_DEV_colors[i] = p._Color;
					p.X = pos.X;
					p.Y = pos.Y;
					p.SetStartPos(pos.X, pos.Y);
				}
				_settings.DEV_Presentation_BoundSpeed = 3f;
			}
			else if (_settings.DEV_counter == 5)
			{
				_settings.DEV_Presentation_BoundSpeed = 0f;
			}
			else if (_settings.DEV_counter == 100)
			{
				for (int i = 0; i < Points.Length; i++)
				{
					var p = Points[i];
					var pos = _DEV_poses1[i];
					p.SetStartPos(pos.X, pos.Y);
				}
				_settings.DEV_Presentation_BoundSpeed = 0.4f;
			}
			else if (_settings.DEV_counter > 100 && _settings.DEV_counter <= 140)
			{
				var t = (_settings.DEV_counter - 100) / 40f;
				var t2 = Math.Min((_settings.DEV_counter - 100) / 25f, 1);
				_settings.PointRadius = (int)(5 - t2 * 2);
				_settings.ConnectionsWidth = (int)(2.5 - t2 * 1.5);
				_settings.DistanceMax = (int)(180 - t2 * 30);
				_settings.DEV_Presentation_VisibleAlpha = t;
				_settings.DEV_Presentation_BoundSpeed = 0.4f + t * 0.6f;
			}
			else if (_settings.DEV_counter == 141)
			{
				_settings.DEV_ClockFastMode_TicksForChange = 20;
			}
			else if (_settings.DEV_counter > 180 && _settings.DEV_counter <= 200)
			{
				var t = (_settings.DEV_counter - 180) / 20f;
				for (int i = 0; i < Points.Length; i++)
				{
					var p = Points[i];
					var r = 255;
					var g = 0 + (i + 197) * 63 % 255;
					var b = 0 + i * 89 % 100;
					p._Color = Color.FromArgb(255,
						(int)(p._Color.R + (r - p._Color.R) * t),
						(int)(p._Color.G + (g - p._Color.G) * t),
						(int)(p._Color.B + (b - p._Color.B) * t)
					);
				}
				_settings.ConnectionsColor = Color.FromArgb(255,
					(int)(_settings.ConnectionsColor.R + (20 - _settings.ConnectionsColor.R) * t),
					(int)(_settings.ConnectionsColor.G + (170 - _settings.ConnectionsColor.G) * t),
					(int)(_settings.ConnectionsColor.B + (20 - _settings.ConnectionsColor.B) * t)
				);
			}
			else if (_settings.DEV_counter == 240)
			{
				_settings.DEV_Presentation_BoundSpeed = -0.1f;
			}
			else if (_settings.DEV_counter == 250)
			{
				_settings.DEV_Presentation_BoundSpeed = 0;
				_settings.DEV_ClockFastMode_StartTime = "1212";
				_settings.DEV_ClockFastMode_TicksForChange = 3000;
			}
			else if (_settings.DEV_counter > 250 && _settings.DEV_counter <= 290)
			{
				var t = (_settings.DEV_counter - 250) / 40f;
				_settings.PointRadius = (int)(3 + t * 2);
				_settings.ConnectionsWidth = (int)(1 + t * 2);
				_settings.DistanceMax = (int)(150 + t * 50);
				_settings.DEV_Presentation_VisibleAlpha = 1 - t;
				if (_settings.DEV_counter == 290)
				{
					_settings.DEV_ClockFastMode_StartTime = "1200";
				}
			}
			else if (_settings.DEV_counter == 341)
			{
				for (int i = 0; i < Points.Length; i++)
				{
					var p = Points[i];
					var pos = _DEV_poses2[i];
					p.SetStartPos(pos.X, pos.Y);
				}
				_settings.DEV_Presentation_BoundSpeed = 0.05f;
			}
			else if (_settings.DEV_counter > 350 && _settings.DEV_counter <= 360)
			{
				var t = (_settings.DEV_counter - 340) / 20f;
				for (int i = 0; i < Points.Length; i++)
				{
					var p = Points[i];
					var c = _DEV_colors[i];
					p._Color = Color.FromArgb(255,
						(int)(p._Color.R + (c.R - p._Color.R) * t),
						(int)(p._Color.G + (c.G - p._Color.G) * t),
						(int)(p._Color.B + (c.B - p._Color.B) * t)
					);
				}
				_settings.ConnectionsColor = Color.FromArgb(255,
					(int)(_settings.ConnectionsColor.R + (0 - _settings.ConnectionsColor.R) * t),
					(int)(_settings.ConnectionsColor.G + (0 - _settings.ConnectionsColor.G) * t),
					(int)(_settings.ConnectionsColor.B + (255 - _settings.ConnectionsColor.B) * t)
				);
			}
			else if (_settings.DEV_counter > 350 && _settings.DEV_counter <= 480)
			{
				var t = (_settings.DEV_counter - 350) / 130f;
				_settings.DEV_Presentation_BoundSpeed = 0.05f + t / 4;
			}
			else if (_settings.DEV_counter > 500 && _settings.DEV_counter <= 520)
			{
				var t = (_settings.DEV_counter - 500) / 20f;
				_settings.DEV_Presentation_BoundSpeed = 0.3f + t * 2;
			}
		}
	}

	public void Dispose()
	{
		foreach (var p in Points)
			p?.Dispose();
	}

	public void DrawConnections(IGraphics g)
	{
		var dMax = Squared(_settings.DistanceMax * Program.SizeMul);
		var dShade = Squared(_settings.DistanceShading * Program.SizeMul);
		for (int i = 0; i < Points.Length; i++)
		{
			var p1 = Points[i];
			if (p1 == null || p1.Alpha == 0) continue;
			for (int j = i + 1; j < Points.Length; j++)
			{
				var p2 = Points[j];
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
				A = (int)(A * _settings.LineAlpha);
				A = (int)(A * Math.Min(p1.Alpha, p2.Alpha));
				var color = Color.FromArgb(A, _settings.ConnectionsColor);
				g.DrawLine(color, _settings.ConnectionsWidth, p1.X, p1.Y, p2.X, p2.Y);
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float Squared(float num) => num * num;
}
