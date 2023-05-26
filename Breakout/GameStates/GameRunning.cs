namespace Breakout.GameStates;

using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Breakout.Entities;
using Breakout.Entities.EffectItems;
using Breakout.Events;
using Breakout.IO;
using Breakout.Levels;
using Breakout.Entities.Board;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private Shuttle shuttle;
    private EntityContainer<Ball> balls;
    private EntityContainer<EffectItem> fallingItems;
    private int lives;
    private readonly int NUM_LEVELS = 4;
    private Level level;
    private EffectItemHandler effectItemHandler;
    private ScoreBoard scoreBoard;
    private LivesBoard livesBoard;

    public static GameRunning GetInstance() {
        return GetInstance(false);
    }

    public static GameRunning GetInstance(bool resetState) {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
        }
        if (resetState) {
            GameRunning.instance.ResetState();
        }
        //Stop the shuttle if it was moving when going to pause menu
        GameRunning.instance.shuttle.Stop();
        return GameRunning.instance;
    }

    public void ResetState() {
        lives = 2;
        InitBoard();
        ChangeLevel();
        InitEffectItems();
    }

    public void InitBoard() {
        this.scoreBoard = new ScoreBoard();
        this.livesBoard = new LivesBoard(lives);
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
        balls = new EntityContainer<Ball>(5);
        balls.AddEntity(new Ball(BallPosOnShuttle(), ballImage));
    }

    public void InitEffectItems() {
        effectItemHandler = EffectItemHandler.GetInstance();
        effectItemHandler.Initialize(shuttle, scoreBoard, balls);
        GameBus.GetBus().Unsubscribe(GameEventType.StatusEvent, effectItemHandler);
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, effectItemHandler);
        fallingItems = new EntityContainer<EffectItem>();
    }

    public void ChangeLevel() {
        scoreBoard.NextLevel();
        if (scoreBoard.level<= NUM_LEVELS) {
            InitShuttle();
            InitBall();
            level = new Level(scoreBoard.level);
        } else {
            GameWon();
        }
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
        balls.RenderEntities();
        level.Render();
        fallingItems.RenderEntities();
        scoreBoard.Render();
        livesBoard.Render();
    }

    public void UpdateState() {
        StateCheker();
        MoveEntities();
        CollidingEntities();
        this.level.countDownBoard.UpdateCountDown();
    }


    private void StateCheker() {
        var Unbreakables = 0;
        foreach (Block block in level.blocks) {
            if (block.build.isUnbreakable) {
                Unbreakables += 1;
            }
        }

        if (level.blocks.CountEntities() == Unbreakables) {
            ChangeLevel();
        }

        if (lives + balls.CountEntities() > 0) {
            if (balls.CountEntities() == 0) {
                InitBall();
                lives--;
                livesBoard.LostLives(1);
            }
        } else {
            GameOver();
        }
        if (this.level.countDownBoard.timeLeft <= 0){
            GameOver();
        }
    }

    private void MoveEntities() {
        shuttle.Move();
        foreach (Ball ball in balls) {
            ball.Move();
            //let the ball follow the shuttle until released
            if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                ball.Shape.SetPosition(BallPosOnShuttle());
            }
        }
        foreach (EffectItem item in fallingItems) {
            item.Move();
        }
    }

    private void CollidingEntities() {
        balls.Iterate(ball => {
            CollisionData ballVsShuttle =
                CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), shuttle.Shape);

            if (ballVsShuttle.Collision) {
                ball.UpdateDirection(ballVsShuttle.CollisionDir, shuttle.GetDirection());
            }

            level.blocks.Iterate(block => {
                CollisionData ballVsblock =
                    CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);

                if (ballVsblock.Collision) {
                    ball.UpdateDirection(ballVsblock.CollisionDir, block.GetDirection());
                    scoreBoard.AddPoints(block.Value);
                    block.LoseHealth(ball.damage);
                    if (block.build.effectItem != null) {
                        fallingItems.AddEntity(block.build.effectItem);
                    }
                }
            });
        });
        // Power-ups and hazards
        fallingItems.Iterate(item => {
            CollisionData itemVsShuttle =
                CollisionDetection.Aabb(item.Shape.AsDynamicShape(), shuttle.Shape);
            if (itemVsShuttle.Collision) {
                ActivateEffectItem(item);
                item.DeleteEntity();
            }
        });
    }

    private void ActivateEffectItem(EffectItem item) {
        if (item is InstantEffectItem instantItem) {
            eventBus.RegisterEvent(instantItem.ActivationEvent);
        }
        if (item is TimedEffectItem timedItem) {
            eventBus.RegisterEvent(timedItem.ActivationEvent);
            eventBus.RegisterTimedEvent(
                timedItem.DeactivationEvent,
                timedItem.TimeLeft
            );
        }
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

