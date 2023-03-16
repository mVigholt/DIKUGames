namespace Galaga;

using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Health {

    private int points;
    private int max;
    private Text display;

    public Health (Vec2F position, Vec2F extent, int startingHealth) {
        points = startingHealth;
        max = startingHealth;
        display = new Text($"Health: {points}", position, extent);
        display.SetColor(new Vec3I(255,0,0));
    }

    public int Points {
        get { return points; }
    }

    public int Max {
        get { return max; }
    }

    // Remember to explaination your choice as to what happens
    // when losing health.
    public void LoseHealth() {
        points--;
        display.SetText($"Health: {points}");
    }

    public void Render() {
        display.RenderText();
    }
}