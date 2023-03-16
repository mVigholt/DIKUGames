namespace Galaga;

using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Health {

    private int points;
    private Text display;

    public Health (Vec2F position, Vec2F extent, int startingHealth) {
        points = startingHealth;
        display = new Text(points.ToString(), position, extent);
    }

    public int Points {
        get { return points; }
    }

    // Remember to explaination your choice as to what happens
    // when losing health.
    public void LoseHealth () {
        points--;
    }

    public void RenderHealth () {
        Console.WriteLine(display);
    }
}