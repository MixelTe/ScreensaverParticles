using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	static class Program
	{
		public static readonly string KeyName = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaverConections";
		public static Settings Settings = new Settings();
		public static List<Rectangle> rectangles = new List<Rectangle>();  // dev for clock parts
		public static float SizeMul = 1;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//var message = "";
			//foreach (var item in args)
			//{
			//	message += item + " | ";
			//}
			//MessageBox.Show(message);
			if (args.Length > 0 && args[0].Substring(0, 2).Equals("/s", StringComparison.InvariantCultureIgnoreCase))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
			}
			else if (args.Length == 0 || args.Length > 0 && args[0].Substring(0, 2).Equals("/c", StringComparison.InvariantCultureIgnoreCase))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form2());
			}
		}

		public static void Shuffle<T>(this T[] array)
		{
			var rnd = new Random();
			var n = array.Length;
			while (n > 1)
			{
				var k = rnd.Next(n--);
				(array[k], array[n]) = (array[n], array[k]);
			}
		}
	}
}
