namespace ScreenSaverParticles;

class CPoint : IDisposable
{
	private readonly Settings _settings;
	public readonly bool Bound;
	public readonly int Group;
	public bool Visible = true;
	public float Alpha = 1;
	private readonly float _alphaSpeed = 0.025f;
	private float _xStart;
	private float _yStart;
	public float X;
	public float Y;
	private float _speed;
	private float _time = 0;
	private float _counter = 0;
	private float _acc = 0;
	private float _direction;
	private float _rotateSpeed = 0;
	public Color _Color;
	private readonly int _width;
	private readonly int _height;

	private readonly int _maxDistanceWhenBound = 10;


	public CPoint(int x, int y, int width, int height, Color color, Settings settings, bool bound, int group = 0)
	{
		_settings = settings;
		Bound = bound;
		Group = group;
		_xStart = x;
		_yStart = y;
		X = x;
		Y = y;
		_width = width;
		_height = height;
		_speed = _settings.SpeedMax / 2;
		_direction = (float)(Random.Shared.Next(360) / 180d * Math.PI);
		_Color = color;
	}

	public void Update()
	{
		ChangeSpeed();
		Move();
		if (Visible) Alpha = Math.Min(Alpha + _alphaSpeed, 1);
		else Alpha = Math.Max(Alpha - _alphaSpeed, 0);
	}
	private void Move()
	{
		X += (float)(Math.Cos(_direction) * _speed);
		Y += (float)(Math.Sin(_direction) * _speed);

		if (X > _width) _direction = (float)(Math.PI - _direction);
		if (X < 0) _direction = (float)(Math.PI - (_direction - Math.PI) + Math.PI);
		if (Y > _height) _direction = (float)(Math.PI - (_direction + Math.PI / 2) - Math.PI / 2);
		if (Y < 0) _direction = (float)(Math.PI - (_direction + Math.PI / 2) - Math.PI / 2);
		X = Math.Max(Math.Min(X, _width), 0);
		Y = Math.Max(Math.Min(Y, _height), 0);

		if (Bound)
		{
			var speed = _settings.DEV_Presentation_BoundSpeed / Program.SizeMul;
			var speedX = speed * (_xStart - X) / _maxDistanceWhenBound;
			var speedY = speed * (_yStart - Y) / _maxDistanceWhenBound;
			X += speedX;
			Y += speedY;
		}
	}
	private void ChangeSpeed()
	{
		_speed += _acc;
		_speed = Math.Max(Math.Min(_speed, _settings.SpeedMax), -_settings.SpeedMax);
		_direction += _rotateSpeed;
		if (_counter > _time)
		{
			_time = Random.Shared.Next(_settings.TimeMin, _settings.TimeMax);
			_counter = 0;
			var nextAcc = Random.Shared.Next(0, (int)_settings.SpeedMax) / 10f;
			if (_speed == _settings.SpeedMax) _acc = -nextAcc;
			else if (_speed == -_settings.SpeedMax) _acc = nextAcc;
			else
			{
				if (Random.Shared.Next(2) == 1) nextAcc *= -1;
				_acc = nextAcc;
			}
			_rotateSpeed = (float)(Random.Shared.Next(0, (int)(_settings.RotateSpeedMax * 360)) / 180d / Math.PI);
			if (Random.Shared.Next(2) == 1) _rotateSpeed *= -1;
		}
		_counter++;
	}

	public void Draw(IGraphics g)
	{
		if (Alpha == 0) return;
		var c = _Color;
		if (Alpha != 1) c = Color.FromArgb((int)(Alpha * 255), c);
		g.FillEllipse(c, X, Y, _settings.PointRadius, _settings.PointRadius);
	}

	public void Dispose()
	{
	}

	public void SetStartPos(int x, int y)
	{
		_xStart = x;
		_yStart = y;
	}
}
