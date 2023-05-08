namespace Breakout.GameStates;

using System;
using System.Collections.Generic;
using System.IO;
using Breakout.Entities;
using Breakout.Events;
using Breakout.IO;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private Player player;
    private Ball ball;
    private EntityContainer<Block> blocks;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;

    }

    public void InitPlayer() {
        Vec2F pos = new Vec2F(0.15f, 0.03f);
        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );
        player = new Player(pos, image);
    }
    public void InitBall() {
        Vec2F playerPosition = player.GetPosition();
        float ballDiameter = 0.03f;
        float ballRadius = ballDiameter/2;
        Vec2F ballExtent = new Vec2F(ballDiameter, ballDiameter);
        // Initialize ball in the middle of the paddel
        Vec2F ballPostiion = new Vec2F(
                playerPosition.X + player.GetExtent().X/2 - ballRadius,
                playerPosition.Y + player.GetExtent().Y/2);
        DynamicShape ballShape = new DynamicShape(
            ballPostiion,
            ballExtent
        );
        IBaseImage ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png")
        );
        ball = new Ball(ballShape, ballImage);
    }

    public void InitializeGameState() {
        InitPlayer();
        InitBall();
        InitLevel();
    }

    public void InitLevel() {
        blocks = LevelLoader.Load("level1.txt");
    }

    public void GameOver() {
        eventBus.RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameLost)
                .Build()
        );
    }

    public void RenderState() {
        player.Render();
        blocks.RenderEntities();
        ball.Render();
    }

    public void ResetState() {
        this.InitializeGameState();
    }

    public void UpdateState() {
        player.Move();
        ball.Move();
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
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.GameStateEvent)
                        .WithStateType(GameStateType.GamePaused)
                        .Build()
                );
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.PlayerEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyPress)
                        .Build()
                );
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.InputEvent)
                        .WithKey(key)
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
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.PlayerEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyRelease)
                        .Build()
                );
                eventBus.RegisterEvent(
                    new EventBuilder()
                        .WithType(GameEventType.InputEvent)
                        .WithKey(key)
                        .WithAction(KeyboardAction.KeyRelease)
                        .Build()
                );
                break;
            default:
                break;
        }
    }

}