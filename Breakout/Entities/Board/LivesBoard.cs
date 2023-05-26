namespace Breakout.Entities.Board;

using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LivesBoard {
    private int livesLeft;
    private Text livesNum;
    private Entity lifeEntity;
    private static readonly Vec2F HEART_EXTENT = new Vec2F(0.08f, 0.08f);
    private static readonly Vec2F HEART_POSITION = new Vec2F(0.01f, 0.0f);

    public LivesBoard(int lives) {
        livesNum = new Text("x 0", new Vec2F(0.1f, -0.18f), new Vec2F(0.25f, 0.25f));
        livesNum.SetColor(new Vec3I(255, 255, 255));
        this.livesLeft = lives;
        livesNum.SetText($"x {this.livesLeft}");
        lifeEntity = new Entity(new StationaryShape (HEART_POSITION, HEART_EXTENT)
                               , Assets.LifeImage);

    }

    public void LostLives(int lives) {
        int livesRemainning = this.livesLeft - lives;
        if (livesRemainning <=0){
            this.livesLeft = 0;
        }else{
            this.livesLeft = livesRemainning ;
        }
        livesNum.SetText($"x {this.livesLeft}");
    }

    public int GetRemainingLives(){
        return this.livesLeft;
    }
    public void Render() {
        livesNum.RenderText();
        lifeEntity.RenderEntity();
    }
}