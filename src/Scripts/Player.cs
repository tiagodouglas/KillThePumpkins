using Godot;

public class Player : KinematicBody2D
{
	private Vector2 _motion;

	[Export]
	private int _speed = 20;
	[Export]
	private int _maxSpeed = 200;
	[Export]
	private float _friction = 0.5f;

	[Signal]
	public delegate void Animate();

	public override void _PhysicsProcess(float delta)
	{
		UpdateMovement();
		AnimatePlayer();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
			if (@event.IsPressed())
			{
				var projectile = (PackedScene)ResourceLoader.Load("res://src/Weapons/Projectile.tscn");
				if (projectile != null)
				{
					var projectileInstance = projectile.Instance();
					var x = (Projectile)projectileInstance;

					GetParent().AddChild(projectileInstance);
					x.ShootAtMousePosition(GlobalPosition);
				}
			}
	}

	public void UpdateMovement()
	{
		// LookAt(GetGlobalMousePosition());
		if (Input.IsActionPressed("move_down") && !Input.IsActionPressed("move_up"))
			_motion.y = Mathf.Clamp(_motion.y + _speed, 0, _maxSpeed);
		else if (Input.IsActionPressed("move_up") && !Input.IsActionPressed("move_down"))
			_motion.y = Mathf.Clamp(_motion.y - _speed, -_maxSpeed, 0);
		else
			_motion.y = Mathf.Lerp(_motion.y, 0, _friction);

		if (Input.IsActionPressed("move_right") && !Input.IsActionPressed("move_left"))
			_motion.x = Mathf.Clamp(_motion.x + _speed, 0, _maxSpeed);
		else if (Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_right"))
			_motion.x = Mathf.Clamp(_motion.x - _speed, -_maxSpeed, 0);
		else
			_motion.x = Mathf.Lerp(_motion.x, 0, _friction);

		MoveAndSlide(_motion);
	}

	public void AnimatePlayer() => EmitSignal(nameof(Animate), _motion);
}
