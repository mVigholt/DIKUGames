namespace Breakout.GameStates;

using System.Collections.Generic;
using DIKUArcade.Graphics;
using Breakout.IO;
using DIKUArcade.Math;

public class GameWon : MenuGameState {
    private static GameWon instance = null;

    public GameWon() : base($"You Win", Assets.backGroundImage, "Main Menu", "Quit") {
        ResetState();
    }

    public static GameWon GetInstance() {
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

    public static GameWon GetInstance(bool resetState) {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
        }
        if (resetState) {
            GameWon.instance.ResetState();
        }
        return GameWon.instance;
    }
}