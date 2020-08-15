using System;
using Godot;

public class bulletStopper : Area2D {

    bulletBrain bulletBrain;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

	public override void _Process(float delta) {

	}

    public void _on_bulletStopper_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "player") && bullet is bullet) {
            bulletBrain.spawnExplosion(GlobalPosition,"player");
            bullet.QueueFree();
            QueueFree();
        }
    }

}