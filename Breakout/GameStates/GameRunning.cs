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
    private EntityContainer<Ball> activeBalls;
    private EntityContainer<Block> blocks;
    private int balls;
    private ScoreBoard scoreBoard;
    private readonly int NUM_LEVELS = 4;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }

    private void InitShuttle() {
        Vec2F playerPosition = new Vec2F(0.5f - Shuttle.STD_EXTEND.X / 2, 0.03f);

        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );

        shuttle = Shuttle.NewShuttle(playerPosition, image);
    }

    private Vec2F BallPosOnShuttle() {
        return new Vec2F(
            shuttle.GetPosition().X + shuttle.GetExtent().X / 2 - Ball.STD_EXTEND.X / 2,
            shuttle.GetPosition().Y + shuttle.GetExtent().Y / 2);
    }

    private void InitBall() {
        IBaseImage ballImage = new Image(
            Path.Combine(PathFinder.Images(), "ball.png"));
        activeBalls = new EntityContainer<Ball>(5);
        activeBalls.AddEntity(new Ball(BallPosOnShuttle(), ballImage));
    }

    private void InitLevel() {
        LevelLoader levelLoader = new LevelLoader("level" + scoreBoard.level.ToString() + ".txt");
        blocks = levelLoader.blocks;
    }

    public void InitScoreBoard() {
        Vec2F position = new Vec2F(0.8f, 0.8f);
        Vec2F extent = new Vec2F(0.2f, 0.2f);
        scoreBoard = new ScoreBoard(position, extent);
    }

    public void ResetState() {
        InitScoreBoard();
        ChangeLevel();
        balls = 2;
    }

    private void ChangeLevel() {
        if (scoreBoard.level <= NUM_LEVELS) {
            scoreBoard.NextLevel();
            InitShuttle();
            InitBall();
            InitLevel();
        } else {GameWon();}
    }

    public void GameOver() {
        eventBus.RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameLost)
                .Build()
        );
    }

    public void GameWon() {
        eventBus.RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameWon)
                .Build()
        );
    }

    public void RenderState() {
        shuttle.Render();
        blocks.RenderEntities();
        activeBalls.RenderEntities();
        scoreBoard.RenderText();
    }

    public void UpdateState() {
        StateCheker();
        MoveEntities();
        CollidingEntities();
    }

    private void StateCheker() {
        var Unbreakables = 0;
        foreach (Block block in blocks) {
            if (block.build.isUnbreakable) {Unbreakables += 1;}
        }

        if (blocks.CountEntities() == Unbreakables) {
            ChangeLevel();
        }

        if (balls + activeBalls.CountEntities() > 0) {
            if (activeBalls.CountEntities() == 0) {
                InitBall();
                balls --;
            }
        } else {
            GameOver();
        }
    }

    private void MoveEntities() {
        shuttle.Move();
        foreach (Ball ball in activeBalls) {
            ball.Move();
            //let the ball follow the shuttle until released
            if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                ball.Shape.SetPosition(BallPosOnShuttle());
            }
        }
        //foreach (Block block in blocks) {block.Move();}
    }

    private void CollidingEntities() {
        activeBalls.Iterate(ball => {
            CollisionData ballVsShuttle =
                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), shuttle.Shape);

            if (ballVsShuttle.Collision) {
                ball.UpdateDirection(ballVsShuttle.CollisionDir, shuttle.GetDirection());
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
                foreach (Ball ball in activeBalls) {
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

