using System;
using Godot;

public class player : Node {

    bulletBrain bulletBrain;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

	public override void _Process(float delta) {

	}

    public void _on_playerHitZone_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && bullet is bullet) {
            bulletBrain.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
        }
    }

}