namespace Breakout.GameStates;

using Breakout.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameWon : IGameState {
    private static GameWon instance = null;
    private Text gameWonText;
    private BackGround backGround;
    private Button mainMenuButton;
    private Button quitButton;
    private ButtonSwitch buttonSwitch;

    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
            GameWon.instance.InitializeGameState();

        }
        return GameWon.instance;
    }

     public void InitializeGameState() {
        gameWonText = new Text("You Win!", new Vec2F(0.3f, 0.2f), new Vec2F(0.4f, 0.4f));
        backGround  = new BackGround(Assets.backGroundImage);
        mainMenuButton = new Button("Main Menu", new Vec2F(0.3f, 0.1f));
        quitButton = new Button("Quit", new Vec2F(0.3f, 0.0f));
        buttonSwitch = new ButtonSwitch (mainMenuButton, quitButton);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                buttonSwitch.KeyPress(key, GameStateType.GameWon);
                break;
            case KeyboardAction.KeyRelease:
                buttonSwitch.KeyRelease(key);
                break;
        }
    }

    public void RenderState() {
        backGround.RenderEntity();
        gameWonText.RenderText();
        foreach (Button i in buttonSwitch.menuButtons){
            i.Render();
        }
    }

    public void ResetState() {
        GameWon.GetInstance().InitializeGameState();
    }

    public void UpdateState() {
        GameWon.GetInstance();
    }
}