using System;
using Godot;

public class player : Node {

    bulletBrain bulletBrain;
    public bool canShoot = true;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
        updateUI();
    }

	public override void _Process(float delta) {

	}

    public void updateUI() {
        var healthAndScore = (Label)GetNode("/root/game/HUD/healthAndScore");
        healthAndScore.Text = "Score: 0 Health: 0";
    }

    // Signals 

    public void _on_playerHitZone_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && bullet is bullet) {
            bulletBrain.CallDeferred("spawnExplosion", bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
        }
    }

}