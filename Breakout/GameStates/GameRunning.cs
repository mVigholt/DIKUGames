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
    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    private int lives;
    private readonly int NUM_LEVELS = 4;
    private Level level;
    public ScoreBoard scoreBoard;
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
        GameRunning.instance.level.shuttle.Stop();
        return GameRunning.instance;
    }

    public void ResetState() {
        lives = 2;
        InitBoard();
        ChangeLevel();
    }

    public void InitBoard() {
        this.scoreBoard = new ScoreBoard();
        this.livesBoard = new LivesBoard(lives);
    }

    public void InitLevel() {
        level = new Level(this.scoreBoard);
    }

    public void ChangeLevel() {
        scoreBoard.NextLevel();
        if (scoreBoard.level <= NUM_LEVELS) {
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

    public void UpdateState() {
        StateCheker();
        MoveEntities();
        CollidingEntities();
        if (this.level.countDownBoard != null) {
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

        if (lives + level.balls.CountEntities() > 0) {
            if (level.balls.CountEntities() == 0) {
                level.LoadEntity();
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
        level.Move();
    }

    private void CollidingEntities() {
        this.level.shuttleAndBall.ballVsShuttleCollide();
        this.level.ballVsBlocksCollide(scoreBoard);
        // Power-ups and hazards
        this.level.itemVsShuttleCollide(scoreBoard);
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

