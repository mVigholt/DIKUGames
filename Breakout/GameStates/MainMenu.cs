namespace Breakout.GameStates;
using Breakout.IO;

/// <summary> Main menu state </summary>
public class MainMenu : GameStateFactory {
    private static MainMenu instance = null;
    public MainMenu() : base("", Assets.mainMenuImage,"New Game", "Quit") {}

    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.ResetState();
        }
        return MainMenu.instance;
    }
}

