using System;
using Godot;

public class bulletBrain : Node {

    scenes scenes = new scenes();
    Timer enemySpawner;
    public float spawnInterval = 0;
    [Export] public float maxSpawnInterval = 4;
    [Export] public float minSpawnInterval = 0.5f;
    [Export] public float spawnIntervalDecrease = 0.2f;
    [Export] public int playerBulletSpeed = 300;
    [Export] public int enemyBulletSpeed = 250;

    public override void _Ready() {
        enemySpawner = (Timer)GetNode("enemySpawner");
        spawnInterval = maxSpawnInterval;
    }

    public void increaseDifficulty() {
        var newSpawnInterval = spawnInterval - spawnIntervalDecrease;
        newSpawnInterval = Math.Max(newSpawnInterval, minSpawnInterval);
        enemySpawner.WaitTime = newSpawnInterval;
        enemySpawner.Start();
    }

    public void spawnEnemy() {
        Vector2 spawnPosition = new Vector2(Convert.ToSingle(GD.RandRange(0, 1000)), -30);
        Vector2 targetPosition = new Vector2(Convert.ToSingle(GD.RandRange(0, 1000)), 550);
        spawnBullet(spawnPosition,targetPosition,"enemy");
    }

    public void spawnBullet(Vector2 spawnPosition, Vector2 targetPosition, string animationName) {
        
        // Spawn bullet at position, and look at target position
        var bullet = (bullet)scenes._sceneBullet.Instance();
        GetNode("/root/game/bullets").AddChild(bullet);
        bullet.GlobalPosition = spawnPosition;
        bullet.LookAt(targetPosition);

        // Set the bullet animation
        var bulletSprite = (AnimatedSprite)bullet.GetNode("AnimatedSprite");
        bulletSprite.Play(animationName);

        // Decides it's speed
        if(animationName == "player") {
            bullet.speed = playerBulletSpeed;
        } else if(animationName == "enemy") {
            bullet.speed = enemyBulletSpeed;
        }
    }
    
    public void spawnExplosion(Vector2 spawnPosition, string animationName) {

         // Spawn explosion at position
        var explosion = (Area2D)scenes._sceneExplosion.Instance();
        GetNode("/root/game/bullets").AddChild(explosion);
        explosion.GlobalPosition = spawnPosition;

        // Set the explosion animation
        var explosionSprite = (AnimatedSprite)explosion.GetNode("AnimatedSprite");
        explosionSprite.Play(animationName);

    }

	public override void _Process(float delta) {

	}

    // Signals

    public void _on_enemySpawner_timeout() {
        spawnEnemy();
    }

}