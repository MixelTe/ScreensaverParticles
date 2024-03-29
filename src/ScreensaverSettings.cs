﻿using Microsoft.Win32;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class Settings
	{
		//main
		public int Density = 10;
		public Color BackgroundColor = Color.Black;

		//clock mode
		public bool ClockMode = false;
		public float ClockSize = 0.9f;

		//points
		public bool DrawPoints = true;
		public int PointRadius = 8;
		public float SpeedMax = 5;
		public float RotateSpeedMax = 0.05f;
		public int TimeMin = 5;
		public int TimeMax = 50;
		//points color
		public int ColorMin = 0;
		public int ColorMax = 360;
		public float ColorLMin = 0.4f;
		public float ColorLMax = 0.6f;

		//connections
		public bool DrawConections = true;
		public Color ConnectionsColor = Color.Blue;
		public float LineAlpha = 0.8f;
		public int ConnectionsWidth = 1;
		public int DistanceMax = 150;
		public int DistanceShading = 100;


		//dev
		public bool DEV_ClockFakeTimeMode = false;
		public int DEV_ClockFastMode_TicksForChange = 60;
		public string DEV_ClockFastMode_StartTime = "1200";
		public int DEV_PointsPerDot = 6;
	}
}