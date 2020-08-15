using System;
using Godot;

public class explosion : Area2D {

    bulletBrain bulletBrain;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

	public override void _Process(float delta) {

	}

    // Signals

    public void _on_AnimatedSprite_animation_finished() {
        QueueFree();
    }

    public void _on_explosion_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        var explosionType = (AnimatedSprite)GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && bullet is bullet) {
            bulletBrain.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
        }
    }
}