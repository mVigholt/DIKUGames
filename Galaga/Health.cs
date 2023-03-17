namespace Galaga;

using System;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;

/// <summary>
/// An object that can hold health information and display it.
/// In a larger application, consider using a design pattern
/// that attaches this to an entity using an intermediary object.
/// Could be the mediator pattern.
/// </summary>
public class Health {

    private int points;
    private int max;
    private Text display;

    public Health (Vec2F position, Vec2F extent, int startingHealth) {
        points = startingHealth;
        max = startingHealth;
        display = new Text($"HP: {points}", position, extent);
        display.SetColor(new Vec3I(255,0,0));
    }

    public int Points {
        get { return points; }
    }

    public int Max {
        get { return max; }
    }

    /// <summary> Decrement the health points and update the display. </summary>
    public void LoseHealth(int hp) {
        points -= hp;
        display.SetText($"HP: {points}");
    }

    public void Render() {
        display.RenderText();
    }
}