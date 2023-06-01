namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Breakout.Entities;
using Breakout.Entities.EffectItems;
using Breakout.Events;
using Breakout.Levels;
using Breakout.Entities.Board;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private Shuttle shuttle;
    private EntityContainer<Ball> balls;
    private EntityContainer<EffectItem> fallingItems = new EntityContainer<EffectItem>();
    private int lives;
    private readonly int NUM_LEVELS = 4;
    private Level level;
    private EffectItemHandler effectItemHandler;
    private ScoreBoard scoreBoard;
    private LivesBoard livesBoard;
    private EntityLoader entityLoader;

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

    public void InitEntityLoader(){
        entityLoader = new EntityLoader();
        this.shuttle = entityLoader.shuttle;
        this.balls = entityLoader.balls;
    }


    public void InitEffectItems() {
        effectItemHandler = EffectItemHandler.GetInstance();
        effectItemHandler.Initialize(shuttle, scoreBoard, balls);
        GameBus.GetBus().Unsubscribe(GameEventType.StatusEvent, effectItemHandler);
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, effectItemHandler);
    }

    public void ChangeLevel() {
        scoreBoard.NextLevel();
        if (scoreBoard.level <= NUM_LEVELS) {
            InitEntityLoader();
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
        level.Render();
        fallingItems.RenderEntities();
        scoreBoard.Render();
        livesBoard.Render();
        entityLoader.Render();
    }

    public void UpdateState() {
        StateCheker();
        MoveEntities();
        CollidingEntities();
        if (this.level.countDownBoard != null){
            this.level.countDownBoard.UpdateCountDown();
        }

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
                InitEntityLoader();
                lives--;
                livesBoard.LostLives(1);
            }
        } else {
            GameOver();
        }
        if (this.level.countDownBoard != null) {
            if (this.level.countDownBoard.timeLeft <= 0) {
                GameOver();
            }
        }

    }

    private void MoveEntities() {
        entityLoader.Move();
        foreach (EffectItem item in fallingItems) {
            item.Move();
        }
        foreach (Block block in this.level.blocks){
            block.Move();
        }
    }

    private void CollidingEntities() {
        entityLoader.ballVsShuttleCollide();
        balls.Iterate(ball => {
            level.blocks.Iterate(block => {
                CollisionData ballVsblock =
                    CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);

                if (ballVsblock.Collision) {
                    ball.UpdateDirection(block.GetDirection(), ballVsblock.CollisionDir);
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
                        ball.UpdateDirection(new Vec2F(X, 1));
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

