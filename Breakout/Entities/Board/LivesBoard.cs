namespace Breakout.Entities.Board;

using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LivesBoard {
    private static readonly Vec2F HEART_EXTENT = new Vec2F(0.08f, 0.08f);
    private static readonly Vec2F HEART_POSITION = new Vec2F(0.01f, 0.0f);
    private static readonly Vec3I LIVE_COLOR = new Vec3I(255, 255, 255);
    private static readonly Vec2F LIVE_POS = new Vec2F(0.1f, -0.18f);
    private static readonly Vec2F LIVE_EXTENT = new Vec2F(0.25f, 0.25f);
    
    private int livesLeft;
    private Text livesTextNum;
    private Entity lifeEntity;

    public LivesBoard(int startLives) {
        livesTextNum = new Text("x 0", LIVE_POS, LIVE_EXTENT);
        livesTextNum.SetColor(LIVE_COLOR);
        this.livesLeft = startLives;
        livesTextNum.SetText($"x {this.livesLeft}");
        lifeEntity = new Entity(new StationaryShape(HEART_POSITION, HEART_EXTENT)
                               , Assets.LifeImage);
    }

    public void LostLives(int lives) {
        int livesRemainning = this.livesLeft - lives;
        if (livesRemainning <= 0) {
            this.livesLeft = 0;
        } else {
            this.livesLeft = livesRemainning;
        }
        livesTextNum.SetText($"x {this.livesLeft}");
    }

    public int GetRemainingLives() {
        return this.livesLeft;
    }
    public void Render() {
        livesTextNum.RenderText();
        lifeEntity.RenderEntity();
    }
}