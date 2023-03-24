namespace Galaga.GalagaStates;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class GameWon : IGameState {
    private static GameWon instance = null;
    private Entity backGroundImage;
    private Vec3I gameColor = new Vec3I(255, 0, 0);
    private Vec3I activeColor = new Vec3I(255, 255, 255);
    private Vec3I inactiveColor = new Vec3I(51, 153, 255);
    private Text gameWon;
    private Text mainMenu;

    public static GameWon GetInstance() {
        if (GameWon.instance == null) {
            GameWon.instance = new GameWon();
            GameWon.instance.InitializeGameState();
        }
        return GameWon.instance;
    }
    public void InitializeGameState() {
        Vec2F backGroundPos = new Vec2F(0.0f, 0.0f);
        Vec2F backGroundExtent = new Vec2F(1.0f, 1.0f);
        Shape backGroundShape = new StationaryShape(backGroundPos, backGroundExtent);
        backGroundImage = new Entity(backGroundShape, Assets.backGroundImage);

        Vec2F GameWonExtend = new Vec2F(0.4f, 0.4f);
        gameWon = new Text("You Win!", new Vec2F(0.3f, 0.2f), GameWonExtend);
        gameWon.SetColor(gameColor);

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
                GalagaBus.GetBus().RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithObject(GameStateType.MainMenu)
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
        gameWon.RenderText();
        mainMenu.RenderText();
    }

    public void ResetState() {
        GameWon.GetInstance().InitializeGameState();
    }

    public void UpdateState() {
        GameWon.GetInstance();
    }
}