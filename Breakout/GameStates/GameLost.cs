namespace Breakout.GameStates;

using System.Collections.Generic;
using Breakout.IO;
using DIKUArcade.Math;
using DIKUArcade.Graphics;


public class GameLost : GameState {
    private static GameLost instance = null;
    public GameLost() :
         base("Game Over", Assets.backGroundImage, "Main Menu", "Quit") {
            ResetState();
    }

    public static GameLost GetInstance() {
        return GetInstance(false);
    }

    public override void ResetState(){
        this.texts = new List<Text>{};
        this.texts.Add(titleText);
        int score = GameRunning.GetInstance(false).scoreBoard.GetRemainingPoint();
        this.AddText($"Score: {score}",
                new Vec2F(0.3f, 0.1f),
                TEXT_EXTENT);
        base.ResetState();
    }

    public static GameLost GetInstance(bool resetState) {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
        }
        if (resetState) {
            GameLost.instance.ResetState();
        }
        return GameLost.instance;
    }
}