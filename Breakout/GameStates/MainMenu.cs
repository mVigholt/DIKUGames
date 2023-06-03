namespace Breakout.GameStates;
using Breakout.IO;

/// <summary> Main menu state </summary>
public class MainMenu : GameState {
    private static MainMenu instance = null;
    public MainMenu() : base("", Assets.mainMenuImage,"New Game", "Quit") {}

    public static MainMenu GetInstance() {
        return GetInstance(false);
    }

    public static MainMenu GetInstance(bool resetState) {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
        }
        if (resetState) {
            MainMenu.instance.ResetState();
        }
        return MainMenu.instance;
    }
}

