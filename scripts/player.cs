using System;
using Godot;

public class player : Node {

    bulletBrain bulletBrain;
    public bool canShoot = true;
    public bool gameOver = false;
    public int health = 3;
    public int score = 0;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
        updateUI();
    }

     public override void _Input(InputEvent _inputEvent) {
        if(_inputEvent.IsActionPressed("click") && gameOver == true) {
            GetTree().ReloadCurrentScene();
        }
    }

	public override void _Process(float delta) {

	}

    public void hitPlayer(int damageAmount = 1) {
        health = Math.Max(health-damageAmount,0);
        updateUI();

        // Game over
        if (health <= 0 && gameOver != true) {
            gameOver = true;
            canShoot = false;
            var gameOverScreen = (Node2D)GetNode("/root/game/HUD/gameOverScreen");
            gameOverScreen.Visible = true;

            var cannon = (Node2D)GetNode("/root/game/foreground/cannon");
            bulletBrain.CallDeferred("spawnExplosion", cannon.GlobalPosition, "enemy");
            cannon.QueueFree();
        }
    }

    public void addScore(int scoreAmount = 1) {
        score += scoreAmount;
        updateUI();

        // Increases difficulty
        bulletBrain.increaseDifficulty();
    }

    public void updateUI() {
        var healthAndScore = (Label)GetNode("/root/game/HUD/healthAndScore");
        var newHudText = "HEALTH: " + health + "     " + "SCORE: " + score;
        healthAndScore.Text = newHudText;
    }

    // Signals 

    public void _on_playerHitZone_area_entered(Area2D bullet) {
        var bulletType = (AnimatedSprite)bullet.GetNodeOrNull("AnimatedSprite");
        if((bulletType != null) && (bulletType.Animation == "enemy") && bullet is bullet) {
            bulletBrain.CallDeferred("spawnExplosion", bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
            hitPlayer();
        }
    }

}