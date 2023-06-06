namespace Breakout.Entities.Board;

using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// This class inheritates Text class to show the how much
/// score the player has won for the game.
///</summary>
public class ScoreBoard : Text {

    private static readonly Vec2F SCOREBOARD_POS = new Vec2F(0.7f, 0.75f);
    private static readonly Vec2F SCOREBOARD_EXTENT = new Vec2F(0.25f, 0.25f);
    private static readonly Vec3I SCOREBOARD_COLOR = new Vec3I(51, 153, 255);

    private int points = 0;
    public int Level { get; private set; } = 0;

    public ScoreBoard() :
        base("Score: 0", SCOREBOARD_POS, SCOREBOARD_EXTENT) {
        SetColor(SCOREBOARD_COLOR);
    }

    ///<summary>
    ///The function is to add or deduct points, and the update the score shown
    ///on the LivesBoard in each level.
    ///</summary>
    ///<param name = "points"> the points that about to add to the total score</param>
    public void AddPoints(int points) {
        this.points += points;
        SetText($"Score: {this.points}");
    }

    public void Render() {
        RenderText();
    }

    public int GetRemainingPoint() {
        return points;
    }
    public void NextLevel() {
        Level += 1;
    }
}