using Godot;
using System;

public class cannonBarrel : Sprite {

    public override void _Input(InputEvent _inputEvent) {
        if(_inputEvent.IsActionPressed("click")) {
            GD.Print("Click click");
        }
    }

}
