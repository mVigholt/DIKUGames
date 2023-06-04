namespace Breakout.GameStates;

using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Entities;
using Breakout.Events;
using Breakout.Levels;
using Breakout.Entities.Board;

public class GameRunning : IGameState {
    private readonly int NUM_LEVELS = 4;
    
    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    public Level level;
    public ScoreBoard scoreBoard;
    private LivesBoard livesBoard;
    
    public int Lives { get; private set; }

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
        GameRunning.instance.level.shuttle.Stop();
        return GameRunning.instance;
    }

    public void ResetState() {
        Lives = 2;
        InitBoard();
        ChangeLevel();
    }

    public void InitBoard() {
        scoreBoard = new ScoreBoard();
        livesBoard = new LivesBoard(Lives);
    }

    public void InitLevel() {
        level = new Level(scoreBoard);
    }

    public void ChangeLevel() {
        scoreBoard.NextLevel();
        if (scoreBoard.Level <= NUM_LEVELS) {
            InitLevel();
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
        scoreBoard.Render();
        livesBoard.Render();
    }

    /// <summary>Methods that should be called each frame</summary>
    public void UpdateState() {
        StateCheker();
        MoveEntities();
        CollidingEntities();
        if (level.CountDownBoard != null) {
            level.CountDownBoard.UpdateCountDown();
        }
    }

    public void LoseLives(int lives) {
        this.Lives -= lives;
        livesBoard.LoseLives(lives);
    }

    private void StateCheker() {
        var numUnbreakables = 0;
        foreach (Block block in level.Blocks) {
            if (block.build.isUnbreakable) {
                numUnbreakables += 1;
            }
        }
        bool levelWon = level.Blocks.CountEntities() == numUnbreakables;
        if (levelWon) {
            ChangeLevel();
        }

        if (Lives + level.balls.CountEntities() > 0) {
            if (level.balls.CountEntities() == 0) {
                level.InitBalls();
                level.InitShuttle();
                LoseLives(1);
            }
        } else {
            GameOver();
        }
        if (level.CountDownBoard != null) {
            if (level.CountDownBoard.timeLeft <= 0) {
                GameOver();
            }
        }
    }

    private void MoveEntities() {
        level.Move();
    }

    private void CollidingEntities() {
        level.BallVsShuttleCollide();
        level.BallVsBlocksCollide(scoreBoard);
        level.ItemVsShuttleCollide();
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
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
                foreach (Ball ball in level.balls) {
                    if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                        var X = level.shuttle.GetDirection().X;
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

