namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Board;
using DIKUArcade.Physics;
using Breakout.Entities.EffectItems;

public class Level {

    private EntityContainer<EffectItem> fallingItems = new EntityContainer<EffectItem>();
    private BoardsLoader boardsLoader;
    private EffectItemsLoader effectItemsLoader;

    public CountDownBoard CountDownBoard {
        get; private set;
    }
    public LevelBoard LevelBoard {
        get; private set;
    }
    public LevelHandler LevelHandler {
        get; private set;
    }
    public EntityContainer<Block> Blocks {
        get; private set;
    }

    public ShuttleAndBall shuttleAndBall {
        get; set;
    }
    public Shuttle shuttle {
        get; set;
    }
    public EntityContainer<Ball> balls {
        get; set;
    }
    public double? levelTime {
        get; private set;
    }
    public string levelName {
        get; private set;
    }

    public Level(ScoreBoard scoreBoard) {
        int levelNum = scoreBoard.level;
        LevelHandler = new LevelHandler("level" + levelNum.ToString() + ".txt");
        this.levelTime = LevelHandler.levelTime;
        this.levelName = LevelHandler.levelName;
        LoadBlocks();
        LoadBoards();
        LoadEntity();
        LoadEffectItems(scoreBoard);
    }

    private void LoadBlocks() {
        Blocks = LevelHandler.Blocks;
    }

    private void LoadBoards() {
        boardsLoader = new BoardsLoader(this.levelTime, this.levelName);
        this.CountDownBoard = boardsLoader.CountDownBoard;
        this.LevelBoard = boardsLoader.LevelBoard;
    }

    public void LoadEntity() {
        shuttleAndBall = new ShuttleAndBall();
        this.shuttle = shuttleAndBall.shuttle;
        this.balls = shuttleAndBall.balls;
    }

    private void LoadEffectItems(ScoreBoard scoreBoard) {
        effectItemsLoader =
            new EffectItemsLoader(this.shuttle, scoreBoard, this.balls);
    }

    public void BallVsBlocksCollide(ScoreBoard scoreBoard) {
        balls.Iterate(ball => {
            this.Blocks.Iterate(block => {
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
    }

    public void ItemVsShuttleCollide(ScoreBoard scoreBoard) {
        fallingItems.Iterate(item => {
            CollisionData itemVsShuttle =
                CollisionDetection.Aabb(item.Shape.AsDynamicShape(), shuttle.Shape);
            if (itemVsShuttle.Collision) {
                SendEffectEvent(item);
                item.DeleteEntity();
            }
        });
    }

    /// <summary>
    /// Given an EffectItem, register its associated
    /// activation event. If this EffectItem has a duration,
    /// send out a reciprocal deactivation event as well.
    /// </summary>
    private void SendEffectEvent(EffectItem item) {
        if (item is InstantEffectItem instantItem) {
            GameBus.GetBus().RegisterEvent(instantItem.ActivationEvent);
        }
        if (item is TimedEffectItem timedItem) {
            GameBus.GetBus().RegisterEvent(timedItem.ActivationEvent);
            GameBus.GetBus().RegisterTimedEvent(
                timedItem.DeactivationEvent,
                timedItem.TimeLeft
            );
        }
    }

    public void Move() {
        shuttleAndBall.Move();
        foreach (Block block in this.Blocks) {
            block.Move();
        }
        foreach (EffectItem item in fallingItems) {
            item.Move();
        }
    }

    public void Render() {
        Blocks.RenderEntities();
        boardsLoader.Render();
        shuttleAndBall.Render();
        fallingItems.RenderEntities();
    }

}