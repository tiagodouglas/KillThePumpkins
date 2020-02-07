using Godot;

public class Player : KinematicBody2D
{
	private Vector2 motion = new Vector2();

	public int Speed = 20;
	public int MaxSpeed = 200;
	public float Friction = 0.1f;

	public override void _PhysicsProcess(float delta)
	{
		UpdateMovement();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
			if (@event.IsPressed())
			{
				var projectile = (PackedScene)ResourceLoader.Load("res://src/Weapons/Projectile.tscn");
				var projectileInstance = projectile.Instance();
				var x = (Projectile)projectileInstance;

				GetParent().AddChild(projectileInstance);
				x.ShootAtMousePosition(GlobalPosition);
			}
	}

	public void UpdateMovement()
	{
		// LookAt(GetGlobalMousePosition());
		if (Input.IsActionPressed("move_down") && !Input.IsActionPressed("move_up"))
			motion.y = Mathf.Clamp(motion.y + Speed, 0, MaxSpeed);
		else if (Input.IsActionPressed("move_up") && !Input.IsActionPressed("move_down"))
			motion.y = Mathf.Clamp(motion.y - Speed, -MaxSpeed, 0);
		else
			motion.y = 0;

		if (Input.IsActionPressed("move_right") && !Input.IsActionPressed("move_left"))
			motion.x = Mathf.Clamp(motion.x + Speed, 0, MaxSpeed);
		else if (Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_right"))
			motion.x = Mathf.Clamp(motion.x - Speed, -MaxSpeed, 0);
		else
			motion.x = 0;

		var playerAnimation = (AnimatedSprite)GetNode("PlayerAnimation");

		playerAnimation.FlipH = motion.x < 0;
		playerAnimation.Play(motion.y != 0 || motion.x != 0 ? "walk" : "idle");

		MoveAndSlide(motion);
	}

}
