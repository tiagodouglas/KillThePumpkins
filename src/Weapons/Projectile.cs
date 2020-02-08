using Godot;

public class Projectile : RigidBody2D
{

	[Export]
	private float Speed = 700.0f;
	private Timer _timer;


	public override void _Ready()
	{
		_timer = (Timer)GetNode("Timer");
		_timer.Start();

	}

	public override void _PhysicsProcess(float delta)
	{
		if (_timer.IsStopped())
			QueueFree();

		var colliders = GetCollidingBodies();

		if (colliders != null)
			foreach (var collider in colliders) 
			{
				var obj = (CollisionObject2D)collider;
				if (obj.IsInGroup("Enemy"))
				{
					obj.QueueFree();
					QueueFree();
				}
			}
				
	}

	public void ShootAtMousePosition(Vector2 startPosition)
	{
		GlobalPosition = startPosition;
		var direction = (GetGlobalMousePosition() - startPosition).Normalized();
		LinearVelocity = direction * Speed;
	}

}





