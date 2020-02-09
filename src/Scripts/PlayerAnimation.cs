using System;
using Godot;

public class PlayerAnimation : AnimatedSprite
{
	private void _on_Player_Animate(Vector2 motion)
	{
		FlipH = motion.x < 0;
		if (Math.Abs(motion.x) > 0.1 || Math.Abs(motion.y) > 0.1)
			Play("walk");
		else
			Play("idle");
	}
}


