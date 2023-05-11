namespace Breakout.Entities;

using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class ScoreBoard : Text {

    private int points = 0;

    public int level {get; private set;} = 0;

    public ScoreBoard(Vec2F pos, Vec2F extent) : base("Score: 0", pos, extent) {
        SetColor(new Vec3I(255, 0, 0));
    }

    /** Add a positive number of points to the scoreboard */
    public void AddPoints(int points) {
        // if (points < 0) {
        //     throw new ArgumentException(
        //         "You cannot add negative points");
        // }
        this.points += System.Math.Abs(points);
        SetText($"Score: {this.points}");
    }

    public void NextLevel() {
        this.level += 1;
    }
}