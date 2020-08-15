using Godot;
using System;

public class cannonBarrel : Sprite {

    bulletBrain bulletBrain;

    public override void _Ready() {
        bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

    public override void _Input(InputEvent _inputEvent) {
        if(_inputEvent.IsActionPressed("click")) {
            bulletBrain.spawnBullet(GlobalPosition, GetGlobalMousePosition(), "player");
        }
    }

}
