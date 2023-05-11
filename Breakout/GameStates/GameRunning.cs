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
    private Shuttle shuttle;
    private EntityContainer<Ball> balls;
    private EntityContainer<Block> blocks;

    private ScoreBoard scoreBoard;

    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;

    }

    private void InitPlayer() {
        Vec2F playerPosition = new Vec2F(0.5f - Shuttle.STD_EXTEND.X / 2, 0.03f);

        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );

        shuttle = Shuttle.NewPlayer(playerPosition, image);
    }

    private Vec2F BallPosOnPlayer() {
        return new Vec2F(
            shuttle.GetPosition().X + shuttle.GetExtent().X / 2 - Ball.STD_EXTEND.X / 2,
            shuttle.GetPosition().Y + shuttle.GetExtent().Y / 2);
    }

    private void InitBall() {
        IBaseImage ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png"));

        balls = new EntityContainer<Ball>(5);
        balls.AddEntity(new Ball(BallPosOnPlayer(), ballImage));
    }

    private void InitLevel() {
        blocks = LevelLoader.Load("level1.txt");
    }

    public void InitScoreBoard() {
        Vec2F position = new Vec2F(0.8f, 0.8f);
        Vec2F extent = new Vec2F(0.2f, 0.2f);
        scoreBoard = new ScoreBoard(position, extent);
    }

    public void ResetState() {
        InitPlayer();
        InitBall();
        InitLevel();
        InitScoreBoard();
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
        shuttle.Render();
        blocks.RenderEntities();
        balls.RenderEntities();
        scoreBoard.RenderText();
    }

    public void UpdateState() {
        MoveEntities();
        CollidingEntities();
        if (blocks.CountEntities() == 0) {
            ResetState();
        }
    }

    private void MoveEntities() {
        shuttle.Move();
        foreach (Ball ball in balls) {
            ball.Move();
            //let the ball follow the shuttle until released
            if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                ball.Shape.SetPosition(BallPosOnPlayer());
            }
        }
        //foreach (Block block in blocks) {block.Move();}
    }

    private void CollidingEntities() {
        balls.Iterate(ball => {
            CollisionData ballVsPlayer =
                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), shuttle.Shape);

            if (ballVsPlayer.Collision) {
                ball.UpdateDirection(ballVsPlayer.CollisionDir, shuttle.GetDirection());
            }

            blocks.Iterate(block => {
                CollisionData ballVsblock =
                    CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);

                if (ballVsblock.Collision) {
                    ball.UpdateDirection(ballVsblock.CollisionDir, block.GetDirection());
                    scoreBoard.AddPoints(block.Value);
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
                    if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                        var X = shuttle.GetDirection().X;
                        X = X != 0 ? (X > 0 ? 1 : -1) : 0;
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

