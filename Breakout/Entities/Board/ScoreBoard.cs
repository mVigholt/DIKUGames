namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class ScoreBoard : Text {

    private int points = 0;

    public int level {get; private set;} = 0;
    public ScoreBoard() :
        base("Score: 0", new Vec2F(0.7f, 0.75f), new Vec2F(0.25f, 0.25f)) {
        SetColor(new Vec3I(51, 153, 255));
    }

    /** Add a positive number of points to the scoreboard */
    public void AddPoints(int points) {
        this.points += points;
        SetText($"Score: {this.points}");
    }
    public void Render(){
        this.RenderText();
    }
    public void NextLevel(){
        this.level += 1;
    }
}