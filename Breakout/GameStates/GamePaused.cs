namespace Breakout.GameStates;

using Breakout.IO;


public class GamePaused : GameStateFactory {
    private static GamePaused instance = null;
    public GamePaused() : base("", Assets.backGroundImage,
        "Continue") {
            this.AddButton("Next Level", GameStateType.GameRunning);
            this.AddButton("Main Menu", GameStateType.MainMenu);
            this.AddButton("Quit", null);
    }
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.ResetState();
        }
        return GamePaused.instance;
    }
}

