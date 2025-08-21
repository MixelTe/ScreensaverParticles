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
		private Point[] _DEV_poses1;
		private Point[] _DEV_poses2;
		private Color[] _DEV_colors;

		public Controller(int width, int height)
		{
			_Settings = Program.Settings;
			_Width = width;
			_Height = height;
			_OneNumRange = (int)(_Width * _Height / (500f * 500) * _Settings.Density / Program.SizeMul);
			if (_Settings.ClockMode) _CPoints = new CPoint[_OneNumRange * 4 + _Settings.DEV_PointsPerDot * 2];
			else _CPoints = new CPoint[(int)(_Width * _Height / (500f * 500) * _Settings.Density)];
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

			if (_Settings.DEV_Presentation)
			{
				if (_Settings.DEV_counter == 1)
				{
					_Settings.DEV_ClockFastMode_TicksForChange = 300;
					_Settings.DEV_Presentation_BoundSpeed = 0;
					_Settings.DEV_Presentation_VisibleI = (int)(_CPoints.Length * 0.5f);
					_Settings.DEV_Presentation_VisibleAlpha = 0;
					_Settings.PointRadius = 5;
					_Settings.ConnectionsWidth = 3;
					_Settings.DistanceMax = 200;
					_DEV_poses1 = new Point[_CPoints.Length];
					_DEV_poses2 = new Point[_CPoints.Length];
					_DEV_colors = new Color[_CPoints.Length];
					for (int i = 0; i < _CPoints.Length; i++)
					{
						var p = _CPoints[i];
						var pos = new Point(_Rnd.Next(_Width), _Rnd.Next(_Height));
						_DEV_poses1[i] = new Point((int)p.X, (int)p.Y);
						_DEV_poses2[i] = pos;
						_DEV_colors[i] = p._Color;
						p.X = pos.X;
						p.Y = pos.Y;
						p.SetStartPos(pos.X, pos.Y);
					}
					_Settings.DEV_Presentation_BoundSpeed = 3f;
				}
				else if (_Settings.DEV_counter == 5)
				{
					_Settings.DEV_Presentation_BoundSpeed = 0f;
				}
				else if (_Settings.DEV_counter == 100)
				{
					for (int i = 0; i < _CPoints.Length; i++)
					{
						var p = _CPoints[i];
						var pos = _DEV_poses1[i];
						p.SetStartPos(pos.X, pos.Y);
					}
					_Settings.DEV_Presentation_BoundSpeed = 0.4f;
				}
				else if (_Settings.DEV_counter > 100 && _Settings.DEV_counter <= 140)
				{
					var t = (_Settings.DEV_counter - 100) / 40f;
					var t2 = Math.Min((_Settings.DEV_counter - 100) / 25f, 1);
					_Settings.PointRadius = (int)(5 - t2 * 2);
					_Settings.ConnectionsWidth = (int)(2.5 - t2 * 1.5);
					_Settings.DistanceMax = (int)(180 - t2 * 30);
					_Settings.DEV_Presentation_VisibleAlpha = t;
					_Settings.DEV_Presentation_BoundSpeed = 0.4f + t * 0.6f;
				}
				else if (_Settings.DEV_counter == 141)
				{
					_Settings.DEV_ClockFastMode_TicksForChange = 20;
				}
				else if (_Settings.DEV_counter > 180 && _Settings.DEV_counter <= 200)
				{
					var t = (_Settings.DEV_counter - 180) / 20f;
					for (int i = 0; i < _CPoints.Length; i++)
					{
						var p = _CPoints[i];
						var r = 255;
						var g = 0 + (i + 197) * 63 % 255;
						var b = 0 + i * 89 % 100;
						p._Color = Color.FromArgb(255,
							(int)(p._Color.R + (r - p._Color.R) * t),
							(int)(p._Color.G + (g - p._Color.G) * t),
							(int)(p._Color.B + (b - p._Color.B) * t)
						);
					}
					_Settings.ConnectionsColor = Color.FromArgb(255,
						(int)(_Settings.ConnectionsColor.R + (20 - _Settings.ConnectionsColor.R) * t),
						(int)(_Settings.ConnectionsColor.G + (170 - _Settings.ConnectionsColor.G) * t),
						(int)(_Settings.ConnectionsColor.B + (20 - _Settings.ConnectionsColor.B) * t)
					);
				}
				else if (_Settings.DEV_counter == 240)
				{
					_Settings.DEV_Presentation_BoundSpeed = -0.1f;
				}
				else if (_Settings.DEV_counter == 250)
				{
					_Settings.DEV_Presentation_BoundSpeed = 0;
					_Settings.DEV_ClockFastMode_StartTime = "1212";
					_Settings.DEV_ClockFastMode_TicksForChange = 3000;
				}
				else if (_Settings.DEV_counter > 250 && _Settings.DEV_counter <= 290)
				{
					var t = (_Settings.DEV_counter - 250) / 40f;
					_Settings.PointRadius = (int)(3 + t * 2);
					_Settings.ConnectionsWidth = (int)(1 + t * 2);
					_Settings.DistanceMax = (int)(150 + t * 50);
					_Settings.DEV_Presentation_VisibleAlpha = 1 - t;
					if (_Settings.DEV_counter == 290)
					{
						_Settings.DEV_ClockFastMode_StartTime = "1200";
					}
				}
				else if (_Settings.DEV_counter == 341)
				{
					for (int i = 0; i < _CPoints.Length; i++)
					{
						var p = _CPoints[i];
						var pos = _DEV_poses2[i];
						p.SetStartPos(pos.X, pos.Y);
					}
					_Settings.DEV_Presentation_BoundSpeed = 0.05f;
				}
				else if (_Settings.DEV_counter > 350 && _Settings.DEV_counter <= 360)
				{
					var t = (_Settings.DEV_counter - 340) / 20f;
					for (int i = 0; i < _CPoints.Length; i++)
					{
						var p = _CPoints[i];
						var c = _DEV_colors[i];
						p._Color = Color.FromArgb(255,
							(int)(p._Color.R + (c.R - p._Color.R) * t),
							(int)(p._Color.G + (c.G - p._Color.G) * t),
							(int)(p._Color.B + (c.B - p._Color.B) * t)
						);
					}
					_Settings.ConnectionsColor = Color.FromArgb(255,
						(int)(_Settings.ConnectionsColor.R + (0 - _Settings.ConnectionsColor.R) * t),
						(int)(_Settings.ConnectionsColor.G + (0 - _Settings.ConnectionsColor.G) * t),
						(int)(_Settings.ConnectionsColor.B + (255 - _Settings.ConnectionsColor.B) * t)
					);
				}
				else if (_Settings.DEV_counter > 350 && _Settings.DEV_counter <= 480)
				{
					var t = (_Settings.DEV_counter - 350) / 130f;
					_Settings.DEV_Presentation_BoundSpeed = 0.05f + t / 4;
				}
				else if (_Settings.DEV_counter > 500 && _Settings.DEV_counter <= 520)
				{
					var t = (_Settings.DEV_counter - 500) / 20f;
					_Settings.DEV_Presentation_BoundSpeed = 0.3f + t * 2;
				}
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
			var dMax = Squared(_Settings.DistanceMax * Program.SizeMul);
			var dShade = Squared(_Settings.DistanceShading * Program.SizeMul);
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
}
