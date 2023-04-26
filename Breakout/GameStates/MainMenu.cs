namespace Breakout.GameStates;

using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Events;
using Breakout.BreakoutEntities;

/// <summary> Main menu state </summary>
public class MainMenu : IGameState {
    private static MainMenu instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private Entity backGroundImage;
    private Text[] menuButtons;
    private int activeMenuButton;
    private int maxMenuButtons;
    private Vec3I activeColor = new Vec3I(255, 255, 255);
    private Vec3I inactiveColor = new Vec3I(51, 153, 255);
    public static MainMenu GetInstance() {
        if (MainMenu.instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.InitializeGameState();
        }
        return MainMenu.instance;
    }

    private void InitializeGameState() {
        Vec2F backGroundPos = new Vec2F(0.0f, 0.0f);
        Vec2F backGroundExtent = new Vec2F(1.0f, 1.0f);
        Shape backGroundShape = new StationaryShape(backGroundPos, backGroundExtent);
        backGroundImage = new Entity(backGroundShape, Assets.mainMenuImage);

        Vec2F menuExtend = new Vec2F(0.3f, 0.3f);
        Text newGame = new Text("New Game", new Vec2F(0.2f, 0.4f), menuExtend);
        Text quit = new Text("Quit", new Vec2F(0.2f, 0.3f), menuExtend);

        menuButtons = new Text[] { newGame, quit };
        maxMenuButtons = menuButtons.Length;
        activeMenuButton = 0;
        foreach (Text i in menuButtons) {
            i.SetColor(inactiveColor);
        }
        menuButtons[activeMenuButton].SetColor(activeColor);
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

    private void KeyPress(KeyboardKey key){
        switch (key){
            case KeyboardKey.Up:
                foreach (Text i in menuButtons) {
                    i.SetColor(inactiveColor);
                }
                activeMenuButton = Math.Max(0, activeMenuButton - 1);
                menuButtons[activeMenuButton].SetColor(activeColor);
                break;
            case KeyboardKey.Down:
                foreach (Text i in menuButtons) {
                    i.SetColor(inactiveColor);
                }
                activeMenuButton = Math.Min(maxMenuButtons - 1, activeMenuButton + 1);
                menuButtons[activeMenuButton].SetColor(activeColor);
                break;
            case KeyboardKey.Enter:
                switch (activeMenuButton) {
                    case (0):
                        eventBus.RegisterEvent(
                            new EventBuilder()
                                .WithType(GameEventType.GameStateEvent)
                                .WithStateType(GameStateType.GameRunning)
                                .Build()
                        );
                        break;
                    case (1):
                        eventBus.RegisterEvent(
                            new EventBuilder()
                                .WithType(GameEventType.WindowEvent)
                                .WithKey(key)
                                .WithAction(KeyboardAction.KeyPress)
                                .Build()
                        );
                        break;
                }
            break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key){
        switch(key){
            default:
                break;
        }
    }


    public void RenderState() {
        backGroundImage.RenderEntity();
        foreach (var i in menuButtons) {
            i.RenderText();
        }
    }

    public void ResetState() {
        MainMenu.instance.InitializeGameState();
    }

    public void UpdateState() {
        MainMenu.GetInstance();
    }
}

