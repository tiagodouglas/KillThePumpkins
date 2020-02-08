using Godot;

public class Enemy : KinematicBody2D
{
	[Export]
	private int Speed = 60;

	private KinematicBody2D Player;

	public override void _Ready()
	{
		Player = (KinematicBody2D)GetParent().GetParent().GetNode("Player");

	}

	public override void _PhysicsProcess(float delta)
	{
		var direction = (Player.GlobalPosition - GlobalPosition).Normalized();
		MoveAndCollide(direction * Speed * delta);
	}
}
