namespace Galaga.GalagaStates;
using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

public class MainMenu : IGameState {
    private static MainMenu instance = null;
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
        Image image = new Image(Path.Combine("Assets", "Images", "TitleImage.png"));
        backGroundImage = new Entity(backGroundShape, image);
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
        switch (action, key) {
            case (KeyboardAction.KeyPress, KeyboardKey.Up):
                foreach (Text i in menuButtons) {
                    i.SetColor(inactiveColor);
                }
                activeMenuButton = Math.Max(0, activeMenuButton - 1);
                menuButtons[activeMenuButton].SetColor(activeColor);
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Down):
                foreach (Text i in menuButtons) {
                    i.SetColor(inactiveColor);
                }
                activeMenuButton = Math.Min(maxMenuButtons - 1, activeMenuButton + 1);
                menuButtons[activeMenuButton].SetColor(activeColor);
                break;

            case (KeyboardAction.KeyPress, KeyboardKey.Enter):
                switch (activeMenuButton) {
                    case (0):
                        GalagaBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GameRunning"
                        });
                        break;
                    case (1):
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                Message = "Quit",
                            });
                        break;
                }
                break;
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

