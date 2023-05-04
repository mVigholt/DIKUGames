namespace Breakout.GameStates;

using Breakout.Entities;
using Breakout.Events;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameLost : IGameState {
    private static GameLost instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private Entity backGroundImage;
    private Vec3I gameColor = new Vec3I(255, 0, 0);
    private Vec3I activeColor = new Vec3I(255, 255, 255);
    private Vec3I inactiveColor = new Vec3I(51, 153, 255);
    private Text gameLost;
    private Text mainMenu;

    public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
            GameLost.instance.InitializeGameState();
        }
        return GameLost.instance;
    }
    public void InitializeGameState() {
        Vec2F backGroundPos = new Vec2F(0.0f, 0.0f);
        Vec2F backGroundExtent = new Vec2F(1.0f, 1.0f);
        Shape backGroundShape = new StationaryShape(backGroundPos, backGroundExtent);
        backGroundImage = new Entity(backGroundShape, Assets.backGroundImage);

        Vec2F gameLostExtend = new Vec2F(0.4f, 0.4f);
        gameLost = new Text("Game Over", new Vec2F(0.3f, 0.2f), gameLostExtend);
        gameLost.SetColor(gameColor);

        Vec2F mainMenuExtend = new Vec2F(0.3f, 0.3f);
        mainMenu = new Text("Main Menu", new Vec2F(0.3f, 0.1f), mainMenuExtend);
        mainMenu.SetColor(inactiveColor);
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                this.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                this.KeyRelease(key);
                break;
        }
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                mainMenu.SetColor(activeColor);
                break;
            case KeyboardKey.Enter:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithStateType(GameStateType.MainMenu)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                );
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            default:
                break;
        }
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        gameLost.RenderText();
        mainMenu.RenderText();
    }

    public void ResetState() {
        GameLost.GetInstance().InitializeGameState();
    }

    public void UpdateState() {
        GameLost.GetInstance();
    }
}