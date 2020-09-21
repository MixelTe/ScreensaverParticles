using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class ScreensaverSettings
	{
		private static readonly string _KeyName = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaverConections";
		public static int D_Density = 20;

		public static readonly int D_ColorMin = 0;
		public static readonly int D_ColorMax = 360;
		public static readonly float D_ColorLMin = 0.4f;
		public static readonly float D_ColorLMax = 0.6f;

		public static readonly bool D_DrawConections = true;
		public static readonly Color D_ConnectionsColor = Color.Blue;
		public static readonly int D_ConnectionsWidth = 1;
		public static readonly int D_PointRadius = 8;
		public static readonly double D_SpeedMax = 5;
		public static readonly double D_RotateSpeedMax = 0.05;
		public static readonly int D_DistanceMax = 150;
		public static readonly int D_DistanceShading = 100;
		public static readonly float D_LineAlpha = 0.8f;
		public static readonly int D_TimeMin = 5;
		public static readonly int D_TimeMax = 50;


		public int Density = D_Density;
		public int ColorMin = D_ColorMin;
		public int ColorMax = D_ColorMax;
		public float ColorLMin = D_ColorLMin;
		public float ColorLMax = D_ColorLMax;
		
		public readonly bool DrawConections = D_DrawConections;
		public readonly Color ConnectionsColor = D_ConnectionsColor;
		public readonly int ConnectionsWidth = D_ConnectionsWidth;
		public readonly int PointRadius = D_PointRadius;
		public readonly double SpeedMax = D_SpeedMax;
		public readonly double RotateSpeedMax = D_RotateSpeedMax;
		public readonly int DistanceMax = D_DistanceMax;
		public readonly int DistanceShading = D_DistanceShading;
		public readonly float LineAlpha = D_LineAlpha;
		public readonly int TimeMin = D_TimeMin;
		public readonly int TimeMax = D_TimeMax;


		public void Save()
		{
			Registry.SetValue(_KeyName, "Density", Density);
		}
		public void Load()
		{
			var density = Registry.GetValue(_KeyName, "Density", Density);

			if (density != null) Density = (int)density;
		}

		public override string ToString()
		{
			return "Density: " + Density;
		}
	}
}