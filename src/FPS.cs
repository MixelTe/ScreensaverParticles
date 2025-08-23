using System.Diagnostics;

namespace ScreenSaverParticles;

class FPS
{
	private readonly object _countLock = new();
	private long _t;
	private long _count;

	public FPS()
	{
		_t = Stopwatch.GetTimestamp();
	}
	public void Increment()
	{
		lock (_countLock)
		{
			_count++;
		}
	}
	public double Value
	{
		get
		{
			var t1 = Stopwatch.GetTimestamp();
			long elapsed;
			long c;

			lock (_countLock)
			{
				elapsed = t1 - _t;
				_t = t1;

				c = _count;
				_count = 0;
			}

			return elapsed == 0 ? 0 : (double) c * Stopwatch.Frequency / elapsed;
		}
	}
}
