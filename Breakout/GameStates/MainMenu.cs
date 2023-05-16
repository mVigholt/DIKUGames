namespace Breakout.GameStates;

using Breakout.IO;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;


/// <summary> Main menu state </summary>
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private BackGround backGround;
    private ButtonSwitch buttonSwitch;
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }

    private void InitializeGameState() {
        backGround = new BackGround(Assets.mainMenuImage);
        Button newGameButton = new Button("New Game", new Vec2F(0.2f, 0.4f));
        Button quitButton = new Button("Quit", new Vec2F(0.2f, 0.3f));
        buttonSwitch = new ButtonSwitch(newGameButton, quitButton);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                buttonSwitch.KeyPress(key, GameStateType.MainMenu);
                break;
            case KeyboardAction.KeyRelease:
                buttonSwitch.KeyRelease(key);
                break;
        }
    }
    public void RenderState() {
        backGround.RenderEntity();
        buttonSwitch.Render();
    }

    public void ResetState() {
        MainMenu.instance.InitializeGameState();
    }

    public void UpdateState() {
        MainMenu.GetInstance();
    }
}

