using Microsoft.Win32;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class ScreensaverSettings
	{
		public Settings Settings = new Settings();

		//public void Save()
		//{
		//	RegSerializer.Save(Settings);
		//}
		//public void Load()
		//{

		//}

		//public override string ToString()
		//{
		//	return "Density: " + Density;
		//}

		//private void SaveOne(string key, int variable)
		//{
		//	Registry.SetValue(_KeyName, key, variable);
		//}
		//private void SaveOne(string key, float variable)
		//{
		//	var v = variable.ToString(CultureInfo.InvariantCulture);
		//	Registry.SetValue(_KeyName, key, v);
		//}
		//private void SaveOne(string key, double variable)
		//{
		//	var v = variable.ToString(CultureInfo.InvariantCulture);
		//	Registry.SetValue(_KeyName, key, v);
		//}


		//private void LoadOne(string key, ref int variable)
		//{
		//	var loaded = Registry.GetValue(_KeyName, key, variable);
		//	if (loaded != null) variable = (int)loaded;
		//}
		//private void LoadOne(string key, ref float variable)
		//{
		//	var loaded = Registry.GetValue(_KeyName, key, variable);
		//	float v;
		//	if (loaded != null) variable = (int)loaded / (float)Accuracy;
		//}
		//private void LoadOne(string key, ref double variable)
		//{
		//	var loaded = Registry.GetValue(_KeyName, key, variable);
		//	if (loaded != null) variable = (int)loaded / (double)Accuracy;
		//}
	}

	class Settings
	{
		public int Density = 20;
		public int ColorMin = 0;
		public int ColorMax = 360;
		public float ColorLMin = 0.4f;
		public float ColorLMax = 0.6f;
		public bool DrawConections = true;
		public Color ConnectionsColor = Color.Blue;
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