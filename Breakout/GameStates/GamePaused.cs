namespace Breakout.GameStates;

using Breakout.IO;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;


public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    // private List<Button> menuButtons;
    private ButtonSwitch buttonSwitch;
    private Button continueButton;
    private BackGround backGround;
    private Button mainMenuButton;

    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
    }
    public void InitializeGameState() {
        backGround = new BackGround(Assets.backGroundImage);
        mainMenuButton = new Button("Main Menu", new Vec2F(0.2f, 0.3f));
        continueButton = new Button("Continue", new Vec2F(0.2f, 0.4f));
        buttonSwitch = new ButtonSwitch(mainMenuButton, continueButton);
    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                buttonSwitch.KeyPress(key, GameStateType.GamePaused);
                break;
            case KeyboardAction.KeyRelease:
                buttonSwitch.KeyRelease(key);
                break;
        }
    }
    public void RenderState() {
        backGround.RenderEntity();
        foreach (Button i in buttonSwitch.menuButtons) {
            i.Render();
        }
    }

    public void ResetState() {
        GamePaused.instance.InitializeGameState();
    }

    public void UpdateState() {
        GamePaused.GetInstance();
    }
}

