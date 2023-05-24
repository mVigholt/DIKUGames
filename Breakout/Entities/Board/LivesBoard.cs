namespace Breakout.Entities.Board;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LivesBoard : Text {
    private int livesLeft;

    public LivesBoard(int lives) :
            base("Lives:", new Vec2F(0.01f, -0.15f), new Vec2F(0.25f, 0.25f)) {
        SetColor(new Vec3I(255, 255, 255));
        this.livesLeft = lives;
        SetText($"Lives: {this.livesLeft}");
    }

    public void LostLives(int lives) {
        this.livesLeft -= lives;
        SetText($"Lives: {this.livesLeft}");
    }
    public void Render() {
        this.RenderText();
    }

}