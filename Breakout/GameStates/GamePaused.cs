namespace Breakout.GameStates;

using Breakout.IO;

/// <summary>
/// A GamePaused State class to show some buttons to choose.
/// </summary>
public class GamePaused : MenuGameState {
    private static GamePaused instance = null;
    public GamePaused() : base(Assets.BackgroundImage, "Continue") {
        this.AddButton("Next Level", GameStateType.GameRunning);
        this.AddButton("Main Menu", GameStateType.MainMenu);
        this.AddButton("Quit", null);
    }

    /// <summary>
    /// If the state does not need to be reset, it can also created by
    /// this method. 
    /// </summary>
    public static GamePaused GetInstance() {
        return GetInstance(false);
    }

    /// <summary>
    /// Return an instance of GamePaused.
    /// </summary>
    /// <param name = "resetState">a boolean to check if it is needed
    /// to reset the instance.
    /// </param>
    public static GamePaused GetInstance(bool resetState) {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
        }
        if (resetState) {
            GamePaused.instance.ResetState();
        }
        return GamePaused.instance;
    }
}

