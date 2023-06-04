namespace Breakout.Levels;

using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Physics;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;
using Breakout.Entities.Board;
using Breakout.Entities.EffectItems;
using Breakout.IO;


public class Level {

    private EntityContainer<EffectItem> fallingItems = new EntityContainer<EffectItem>();
    private EffectItemHandler effectItemHandler;
    private LevelBoard levelBoard;
    private BlockLoader blockLoader;
    private double? levelTime;
    private string levelName;

    public CountDownBoard CountDownBoard {
        get; private set;
    }
    public EntityContainer<Block> Blocks {
        get; private set;
    }
    public Shuttle shuttle {
        get; set;
    }
    public EntityContainer<Ball> balls {
        get; set;
    }

    public Level(ScoreBoard scoreBoard) {
        int levelNum = scoreBoard.Level;
        blockLoader = new BlockLoader($"level{levelNum}.txt");
        levelTime = blockLoader.levelTime;
        levelName = blockLoader.levelName;
        Blocks = blockLoader.Blocks;
        InitBoards();
        InitShuttle();
        InitBalls();
        LoadEffectItems(scoreBoard);
    }

    private void InitBoards() {
        if (levelTime != null) {
            CountDownBoard = new CountDownBoard((int) levelTime);
        }
        if (levelName != null) {
            levelBoard = new LevelBoard(levelName);
        }
    }

    public void InitBalls() {
        balls = new EntityContainer<Ball>();
        balls.AddEntity(Ball.At(BallPosOnShuttle()));
    }

    private Vec2F BallPosOnShuttle() {
        return new Vec2F(
            shuttle.GetPosition().X + shuttle.GetExtent().X / 2 - Ball.STD_EXTENT.X / 2,
            shuttle.GetPosition().Y + shuttle.GetExtent().Y / 2);
    }

    public void InitShuttle() {
        Vec2F playerPosition = new Vec2F(0.5f - Shuttle.STD_EXTENT.X / 2, 0.03f);
        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );
        shuttle = Shuttle.NewShuttle(playerPosition, image);
    }

    private void LoadEffectItems(ScoreBoard scoreBoard) {
        effectItemHandler = EffectItemHandler.GetInstance();
        effectItemHandler.Initialize(shuttle, scoreBoard, balls);
    }

    public void BallVsBlocksCollide(ScoreBoard scoreBoard) {
        balls.Iterate(ball => {
            Blocks.Iterate(block => {
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

    public void BallVsShuttleCollide() {
        balls.Iterate(ball => {
            CollisionData ballVsShuttle = CollisionDetection.Aabb(
                ball.Shape.AsDynamicShape(), shuttle.Shape
            );
            if (ballVsShuttle.Collision) {
                ball.UpdateDirection(
                    shuttle.GetDirection(), ballVsShuttle.CollisionDir
                );
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
        shuttle.Move();
        foreach (Ball ball in balls) {
            ball.Move();
            // let the ball follow the shuttle until released
            if (ball.GetDirection().Length() == new Vec2F(0, 0).Length()) {
                ball.Shape.SetPosition(BallPosOnShuttle());
            }
        }
        foreach (Block block in Blocks) {
            block.Move();
        }
        foreach (EffectItem item in fallingItems) {
            item.Move();
        }
    }

    public void Render() {
        Blocks.RenderEntities();
        if (CountDownBoard != null) {
            CountDownBoard.Render();
        }
        if (levelBoard != null) {
            levelBoard.Render();
        }
        shuttle.Render();
        balls.RenderEntities();
        fallingItems.RenderEntities();
    }

}