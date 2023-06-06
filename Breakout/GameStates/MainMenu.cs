namespace Breakout.GameStates;
using Breakout.IO;

/// <summary> Main menu state </summary>
public class MainMenu : MenuGameState {
    private static MainMenu instance = null;
    public MainMenu() : base(Assets.MainMenuImage, "New Game", "Quit") { }


    /// <summary>
    /// If the state does not need to be reset, it can also created by
    /// this method.
    /// </summary>
    public static MainMenu GetInstance() {
        return GetInstance(false);
    }


    /// <summary>
    /// Return an instance of MainMenu.
    /// </summary>
    /// <param name = "resetState">a boolean to check if it is needed
    /// to reset the instance.
    /// </param>
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

