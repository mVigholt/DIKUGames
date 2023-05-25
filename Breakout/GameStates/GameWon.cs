namespace Breakout.GameStates;

using Breakout.IO;

public class GameWon : GameStateFactory {
    private static GameWon instance = null;
    public GameWon() : base("You Win", Assets.backGroundImage, "Main Menu", "Quit") {}
    
    public static GameWon GetInstance() {
        return GetInstance(false);
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