namespace Breakout.Entities;

using System;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

public class ScoreBoard : Text {

    private int points;

    public ScoreBoard(Vec2F pos, Vec2F extent) : base("0", pos, extent) {
        SetColor(new Vec3I(255, 0, 0));
    }

    /** Add a positive number of points to the scoreboard */
    public void AddPoints(int points) {
        if (points < 0) {
            throw new ArgumentException(
                "You cannot add negative points");
        }
        this.points += points;
        SetText($"Score: {this.points}");
    }
}