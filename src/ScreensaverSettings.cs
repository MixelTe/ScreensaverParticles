﻿using Microsoft.Win32;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class Settings
	{
		public int Density = 20;
		public int ColorMin = 0;
		public int ColorMax = 360;
		public float ColorLMin = 0.4f;
		public float ColorLMax = 0.6f;
		public bool DrawConections = true;
		public bool DrawPoints = true;
		public Color ConnectionsColor = Color.Blue;
		public Color BackgroundColor = Color.Black;
		public int ConnectionsWidth = 1;
		public int PointRadius = 8;
		public double SpeedMax = 5;
		public double RotateSpeedMax = 0.05;
		public int DistanceMax = 150;
		public int DistanceShading = 100;
		public float LineAlpha = 0.8f;
		public int TimeMin = 5;
		public int TimeMax = 50;
	}
}