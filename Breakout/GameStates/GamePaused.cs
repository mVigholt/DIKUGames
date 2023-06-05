namespace Breakout.GameStates;

using Breakout.IO;


public class GamePaused : MenuGameState {
    private static GamePaused instance = null;
    public GamePaused() : base(Assets.BackgroundImage, "Continue") {
        this.AddButton("Next Level", GameStateType.GameRunning);
        this.AddButton("Main Menu", GameStateType.MainMenu);
        this.AddButton("Quit", null);
    }

    public static GamePaused GetInstance() {
        return GetInstance(false);
    }

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

