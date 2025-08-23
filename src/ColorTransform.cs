namespace ScreenSaverParticles;

public struct RGB(byte r, byte g, byte b)
{
	public byte R = r;
	public byte G = g;
	public byte B = b;

	public readonly bool Equals(RGB rgb) 
		=> (R == rgb.R) && (G == rgb.G) && (B == rgb.B);
}


public struct HSL(int h, float s, float l)
{
	public int H = h;
	public float S = s;
	public float L = l;

	public readonly bool Equals(HSL hsl) 
		=> (H == hsl.H) && (S == hsl.S) && (L == hsl.L);
}

static class ColorTransform
{
	public static RGB HSL2RGB(double h, double sl, double l)
	{
		double v;
		double r, g, b;

		r = l;   // default to gray
		g = l;
		b = l;
		v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
		if (v > 0)
		{
			double m;
			double sv;
			int sextant;
			double fract, vsf, mid1, mid2;

			m = l + l - v;
			sv = (v - m) / v;
			h *= 6.0;
			sextant = (int)h;
			fract = h - sextant;
			vsf = v * sv * fract;
			mid1 = m + vsf;
			mid2 = v - vsf;
			switch (sextant)
			{
				case 0:
					r = v;
					g = mid1;
					b = m;
					break;
				case 1:
					r = mid2;
					g = v;
					b = m;
					break;
				case 2:
					r = m;
					g = v;
					b = mid1;
					break;
				case 3:
					r = m;
					g = mid2;
					b = v;
					break;
				case 4:
					r = mid1;
					g = m;
					b = v;
					break;
				case 5:
					r = v;
					g = m;
					b = mid2;
					break;
			}
		}
		RGB rgb;
		rgb.R = Convert.ToByte(r * 255.0f);
		rgb.G = Convert.ToByte(g * 255.0f);
		rgb.B = Convert.ToByte(b * 255.0f);
		return rgb;

	}
	public static Color RGBToColor(this RGB rgb, int a)
	{
		return Color.FromArgb(a, rgb.R, rgb.G, rgb.B);
	}
	public static RGB HSLToRGB(this HSL hsl)
	{
		var h = hsl.H / 360d;
		var s = hsl.S / 100d;
		var l = hsl.L / 100d;
		return HSL2RGB(h, s, l);
	}
	public static RGB HSLToRGB_(this HSL hsl)
	{
		byte b, r, g;
		if (hsl.S == 0)
		{
			r = g = b = (byte)(hsl.L * 255);
		}
		else
		{
			float v1, v2;
			float hue = (float)hsl.H / 360;

			v2 = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : ((hsl.L + hsl.S) - (hsl.L * hsl.S));
			v1 = 2 * hsl.L - v2;

			r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0f / 3)));
			g = (byte)(255 * HueToRGB(v1, v2, hue));
			b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0f / 3)));
		}

		return new RGB(r, g, b);
	}

	private static float HueToRGB(float v1, float v2, float vH)
	{
		if (vH < 0)
			vH += 1;

		if (vH > 1)
			vH -= 1;

		if ((6 * vH) < 1)
			return (v1 + (v2 - v1) * 6 * vH);

		if ((2 * vH) < 1)
			return v2;

		if ((3 * vH) < 2)
			return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

		return v1;
	}
}