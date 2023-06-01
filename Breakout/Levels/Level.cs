namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Board;
using DIKUArcade.Physics;
using Breakout.Entities.EffectItems;

public class Level {
    public CountDownBoard countDownBoard {
        get; private set;
    }
    public LevelBoard levelBoard {
        get; private set;
    }
    public LevelHandler levelHandler {
        get; private set;
    }
    public EntityContainer<Block> blocks {
        get; private set;
    }

    public EntityLoader entityLoader {
        get; set;
    }
    public Shuttle shuttle {
        get; set;
    }
    public EntityContainer<Ball> balls {
        get; set;
    }
    private EntityContainer<EffectItem> fallingItems = new EntityContainer<EffectItem>();
    private BoardsLoader boardsLoader;
    private EffectItemsLoader effectItemsLoader;

    public double? levelTime {
        get; private set;
    }
    public string levelName {
        get; private set;
    }


    public Level(int levelNum, ScoreBoard scoreBoard) {
        levelHandler = new LevelHandler("level" + (levelNum).ToString() + ".txt");
        this.levelTime = levelHandler.levelTime;
        this.levelName = levelHandler.levelName;
        LoadBlocks();
        LoadBoards();
        LoadEntity();
        LoadEffectItems(scoreBoard);
    }

    private void LoadBlocks() {
        blocks = levelHandler.blocks;
    }

    private void LoadBoards() {
        boardsLoader = new BoardsLoader(this.levelTime, this.levelName);
        this.countDownBoard = boardsLoader.countDownBoard;
        this.levelBoard = boardsLoader.levelBoard;
    }

    public void LoadEntity() {
        entityLoader = new EntityLoader();
        this.shuttle = entityLoader.shuttle;
        this.balls = entityLoader.balls;
    }

    private void LoadEffectItems(ScoreBoard scoreBoard) {
        effectItemsLoader =
            new EffectItemsLoader(this.shuttle, scoreBoard, this.balls);
    }

    public void ballVsBlocksCollide(ScoreBoard scoreBoard) {
        balls.Iterate(ball => {
            this.blocks.Iterate(block => {
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
    public void itemVsShuttleCollide(ScoreBoard scoreBoard) {
        fallingItems.Iterate(item => {
            CollisionData itemVsShuttle =
                CollisionDetection.Aabb(item.Shape.AsDynamicShape(), shuttle.Shape);
            if (itemVsShuttle.Collision) {
                effectItemsLoader.ActivateEffectItem(item);
                item.DeleteEntity();
            }
        });
    }


    public void Move() {
        entityLoader.Move();
        foreach (Block block in this.blocks) {
            block.Move();
        }
        foreach (EffectItem item in fallingItems) {
            item.Move();
        }
    }

    public void Render() {
        blocks.RenderEntities();
        boardsLoader.Render();
        entityLoader.Render();
        fallingItems.RenderEntities();
    }

}