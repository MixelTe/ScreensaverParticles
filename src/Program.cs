namespace ScreenSaverParticles;

static class Program
{
	public static readonly string KeyName = @"HKEY_CURRENT_USER\Software\MixelTe\ScreenSaverParticles";
	public static Settings Settings = new();
	public static List<Rectangle> rectangles = [];  // dev for clock parts
	public static float SizeMul = 1;

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args)
	{
		ApplicationConfiguration.Initialize();
		//var message = "";
		//foreach (var item in args)
		//{
		//	message += item + " | ";
		//}
		//MessageBox.Show(message);
		if (args.Length > 0 && args[0][..2].Equals("/s", StringComparison.InvariantCultureIgnoreCase))
		{
			Application.Run(new Form1());
		}
		else if (args.Length == 0 || args.Length > 0 && args[0][..2].Equals("/c", StringComparison.InvariantCultureIgnoreCase))
		{
			Application.Run(new Form2());
		}
	}

	public static void Shuffle<T>(this T[] array)
	{
		var n = array.Length;
		while (n > 1)
		{
			var k = Random.Shared.Next(n--);
			(array[k], array[n]) = (array[n], array[k]);
		}
	}
}
