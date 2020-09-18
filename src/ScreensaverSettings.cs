using Microsoft.Win32;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	class ScreensaverSettings
	{
		private static readonly string _KeyName = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaverConections";
		public static int D_Density = 12;

		public int Density = D_Density;


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