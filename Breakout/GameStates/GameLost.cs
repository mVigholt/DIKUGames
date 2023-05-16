namespace Breakout.GameStates;

using Breakout.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameLost : IGameState {
    private static GameLost instance = null;
    private Button quitButton;
    private Button mainMenuButton;
    private Text gameOverText;
    private BackGround backGround;
    private ButtonSwitch buttonSwitch;

    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
            GameLost.instance.InitializeGameState();
        }
        return GameLost.instance;
    }
    public void InitializeGameState() {
        gameOverText = new Text("Game Over", new Vec2F(0.3f, 0.2f), new Vec2F(0.4f, 0.4f));
        backGround  = new BackGround(Assets.backGroundImage);
        mainMenuButton = new Button("Main Menu", new Vec2F(0.3f, 0.1f));
        quitButton = new Button("Quit", new Vec2F(0.3f, 0.0f));
        buttonSwitch = new ButtonSwitch(mainMenuButton, quitButton);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                buttonSwitch.KeyPress(key, GameStateType.GameLost);
                break;
            case KeyboardAction.KeyRelease:
                buttonSwitch.KeyRelease(key);
                break;
        }
    }

    public void RenderState() {
        backGround.RenderEntity();
        foreach (Button i in buttonSwitch.menuButtons){
            i.Render();
        }
    }

    public void ResetState() {
        GameLost.GetInstance().InitializeGameState();
    }

    public void UpdateState() {
        GameLost.GetInstance();
    }
}