namespace Breakout.GameStates;

using Breakout.IO;

public class GameLost : GameStateFactory {
    private static GameLost instance = null;
    public GameLost() : base("Game Over", Assets.backGroundImage, "Main Menu", "Quit") {
    }

    public static GameLost GetInstance() {
        return GetInstance(false);
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