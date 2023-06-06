namespace Breakout.GameStates;

using System.Collections.Generic;
using Breakout.IO;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

/// <summary>
/// A GameLost State class to show information such as "Game Over" Text,
/// The score you have won and some buttons to choose.
/// </summary>
public class GameLost : MenuGameState {
    private static GameLost instance = null;
    public GameLost() :
         base("Game Over", Assets.BackgroundImage, "Main Menu", "Quit") {
        ResetState();
    }

    /// <summary>
    /// If the state does not need to be reset, it can also created by
    /// this method.
    /// </summary>
    public static GameLost GetInstance() {
        return GetInstance(false);
    }

    /// <summary>
    /// Put some information back to original
    /// </summary>
    public override void ResetState() {
        this.texts = new List<Text> { };
        this.texts.Add(titleText);
        int score = GameRunning.GetInstance(false).scoreBoard.GetRemainingPoint();
        this.AddText($"Score: {score}",
                new Vec2F(0.3f, 0.1f),
                TEXT_EXTENT);
        base.ResetState();
    }

    /// <summary>
    /// Return an instance of GameLost.
    /// </summary>
    /// <param name = "resetState">a boolean to check if it is needed
    /// to reset the instance.
    /// </param>
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