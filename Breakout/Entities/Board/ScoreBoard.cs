namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class ScoreBoard : Text {

    private int points = 0;
    public int level { get; private set; } = 0;

    private static readonly Vec2F SCOREBOARD_POS = new Vec2F(0.7f, 0.75f);
    private static readonly Vec2F SCOREBOARD_EXTENT = new Vec2F(0.25f, 0.25f);
    private static readonly Vec3I SCOREBOARD_COLOR = new Vec3I(51, 153, 255);
    public ScoreBoard() :
        base("Score: 0", SCOREBOARD_POS, SCOREBOARD_EXTENT) {
        SetColor(SCOREBOARD_COLOR);
    }

    /** Add a positive or negative number of points to the scoreboard */
    public void AddPoints(int points) {
        this.points += points;
        SetText($"Score: {this.points}");
    }
    
    public void Render() {
        this.RenderText();
    }

    public int GetRemainingPoint() {
        return this.points;
    }
    public void NextLevel() {
        this.level += 1;
    }
}