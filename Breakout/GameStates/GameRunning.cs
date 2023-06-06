namespace Breakout.GameStates;

using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.Entities;
using Breakout.Events;
using Breakout.Levels;
using Breakout.Entities.Board;


/// <summary>
/// A singleton class to show the play state for the game.
/// It contains a livesBoard to show the remaining lives;
/// A ScoreBoard to show the total scores accumulated through the game;
/// A Level class to load different level
/// </summary>
public class GameRunning : IGameState {
    private readonly int NUM_LEVELS = 4;

    private static GameRunning instance = null;
    private GameEventBus eventBus = GameBus.GetBus();
    public Level level;
    public ScoreBoard scoreBoard;
    private LivesBoard livesBoard;

    public int Lives {
        get; private set;
    }

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

    ///<summary>
    ///Reset the game's status for a new Game
    ///</summary>
    public void ResetState() {
        Lives = 2;
        InitBoard();
        ChangeLevel();
    }

    private void InitBoard() {
        scoreBoard = new ScoreBoard();
        livesBoard = new LivesBoard(Lives);
    }

    private void InitLevel() {
        level = new Level(scoreBoard);
    }

    /// <summary>If there is no next level to read,
    /// the player has finished all levels and Game is won  </summary>
    public void ChangeLevel() {
        scoreBoard.NextLevel();
        if (scoreBoard.Level <= NUM_LEVELS) {
            InitLevel();
        } else {
            GameWon();
        }
    }

    private void GameOver() {
        eventBus.RegisterEvent(
            new EventBuilder()
                .WithType(GameEventType.GameStateEvent)
                .WithStateType(GameStateType.GameLost)
                .Build()
        );
    }

    private void GameWon() {
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
        StateChecker();
        MoveEntities();
        CollidingEntities();
        if (level.CountDownBoard != null) {
            level.CountDownBoard.UpdateCountDown();
        }
    }

    ///<summary>
    ///Deduct lives from the remaining lives
    ///</summary>
    public void LoseLives(int lives) {
        this.Lives -= lives;
        livesBoard.LoseLives(lives);
    }


    ///<summary>
    ///Check if the game need to go to other states.
    ///</summary>
    private void StateChecker() {
        // Since the unbreakable cannot be destroyed, we need to
        // count how many unbreakables in each level to
        // check if a new level needed to be loaded or Game is finished
        var numUnbreakables = 0;
        foreach (Block block in level.Blocks) {
            if (block.build.isUnbreakable) {
                numUnbreakables += 1;
            }
        }
        // If there are only unbreakables left, all blocks are destroyed
        // and a new level is loaded.
        bool levelWon = level.Blocks.CountEntities() == numUnbreakables;
        if (levelWon) {
            ChangeLevel();
        }

        // If there is still some blocks and lives left, the
        // game contines in the same level. But the ball
        // will be put to its original position, while
        // the player loses one life.
        if (Lives + level.balls.CountEntities() > 0) {
            if (level.balls.CountEntities() == 0) {
                level.InitBalls();
                LoseLives(1);
            }
        } else {
            GameOver();
        }
        if (level.CountDownBoard != null) {
            //If there are count down meta data in the Ascii file
            //then if the time is finished, the game is over.
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

