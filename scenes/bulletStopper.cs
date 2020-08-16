using System;
using Godot;

public class bulletStopper : Area2D {

    bulletBrain bulletBrain;
    player player;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
        player = (player)GetNode("/root/game/player");
    }

	public override void _Process(float delta) {

	}

    public void _on_bulletStopper_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "player") && bullet is bullet) {
            bulletBrain.CallDeferred("spawnExplosion", GlobalPosition, "player");
            bullet.QueueFree();
            QueueFree();
            player.canShoot = true;
        }
    }

}