namespace Breakout.GameStates;

using System;
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
    private EntityContainer<Ball> balls = new EntityContainer<Ball>(5);
    private EntityContainer<Block> blocks;

    private int points = 0;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;

    }

    private void InitPlayer() {
        Vec2F playerPosition = new Vec2F(0.5f - (Player.STD_EXTEND.X / 2), 0.03f);

        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );

        player = Player.NewPlayer(playerPosition, image);
    }

    private Vec2F BallPosOnPlayer() {
        return new Vec2F(
            player.GetPosition().X + (player.GetExtent().X / 2) - (Ball.STD_EXTEND.X / 2),
            player.GetPosition().Y + (player.GetExtent().Y / 2));
    }

    private void InitBall() {
        IBaseImage ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png"));

        balls.AddEntity(new Ball(BallPosOnPlayer(), ballImage));
    }

    public void InitLevel() {
        blocks = LevelLoader.Load("level1.txt");
    }

    public void ResetState() {
        InitPlayer();
        InitBall();
        InitLevel();
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
        balls.RenderEntities();
    }

    public void UpdateState() {
        MoveEntities();
        CollidingEntities();
    }


    private void MoveEntities() {
        player.Move();
        foreach (Ball ball in balls) {
            ball.Move();
            //let the ball follow the player until released
            if (ball.GetDirection().Length() == new Vec2F(0,0).Length()) {
                ball.Shape.SetPosition(BallPosOnPlayer());
            }
        }
        //foreach (Block block in blocks) {block.Move();}
    }

    private void CollidingEntities() {    
        balls.Iterate(ball => {
            CollisionData ballVsPlayer = 
                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape);

            if (ballVsPlayer.Collision) {
                ball.UpdateDirection(ballVsPlayer.CollisionDir, player.GetDirection());
            }

            blocks.Iterate(block => {
                CollisionData ballVsblock = 
                    CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);
                
                if (ballVsblock.Collision) {
                    ball.UpdateDirection(ballVsblock.CollisionDir, block.GetDirection());
                    points++;
                    Console.WriteLine($"Points: {points}");
                    block.LoseHealth(ball.damage);
                }
            });
        });
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
                break;
            case KeyboardKey.Space:
                //release ball
                foreach (Ball ball in balls) {
                    if (ball.GetDirection().Length() == new Vec2F(0,0).Length()) {
                        var X = player.GetDirection().X;
                        X = (X != 0 ? (X > 0 ? 1 : -1) : 0);
                        ball.UpdateDirection(CollisionDirection.CollisionDirUnchecked, new Vec2F(X, 1));
                    }
                }
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
                break;
            case KeyboardKey.Space:

                break;
            default:
                break;
        }
    }
}

