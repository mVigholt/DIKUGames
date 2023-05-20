namespace Breakout.GameStates;

using Breakout.IO;

public class GameLost : GameStateFactory {
    private static GameLost instance = null;
    public GameLost() : base("Game Over", Assets.backGroundImage, "Main Menu", "Quit") {
    }
    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
        }
        return GameLost.instance;
    }
}