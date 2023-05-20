namespace Breakout.GameStates;

using Breakout.IO;

public class GameWon : GameStateFactory {
    private static GameWon instance = null;
    public GameWon() : base("You Win", Assets.backGroundImage, "Main Menu", "Quit") {}
    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
        }
        return GameWon.instance;
    }
}