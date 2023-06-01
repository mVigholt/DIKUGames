namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Board;
using DIKUArcade.Physics;

public class Level {
    public CountDownBoard countDownBoard {
        get;
        private set;
    }
    public LevelBoard levelBoard {
        get;
        private set;
    }
    public LevelHandler levelHandler {
        get;
        private set;
    }
    public EntityContainer<Block> blocks {
        get;
        private set;
    }

    public EntityLoader entityLoader {get; set;}
    public Shuttle shuttle {get; set;}
    public EntityContainer<Ball> balls {get; set;}

    public double? levelTime {
        get;
        private set;
    }
    public string levelName {
        get;
        private set;
    }

    private BoardsLoader boardsLoader;

    public Level(int levelNum) {
        levelHandler = new LevelHandler("level" + (levelNum).ToString() + ".txt");
        this.levelTime = levelHandler.levelTime;
        this.levelName = levelHandler.levelName;
        LoadBlocks();
        LoadBoards();
        LoadEntity();
    }

    private void LoadBlocks(){
        blocks = levelHandler.blocks;
    }

    private void LoadBoards() {
        boardsLoader = new BoardsLoader(this.levelTime, this.levelName);
        this.countDownBoard = boardsLoader.countDownBoard;
        this.levelBoard = boardsLoader.levelBoard;
    }

    private void LoadEntity(){
        entityLoader = new EntityLoader();
        this.shuttle = entityLoader.shuttle;
        this.balls = entityLoader.balls;
    }

    public void ballVsBlocksCollide(ScoreBoard scoreBoard){
        balls.Iterate(ball => {
            this.blocks.Iterate(block => {
                CollisionData ballVsblock =
                    CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape);

                if (ballVsblock.Collision) {
                    ball.UpdateDirection(block.GetDirection(), ballVsblock.CollisionDir);
                    scoreBoard.AddPoints(block.Value);
                    block.LoseHealth(ball.damage);
                    // if (block.build.effectItem != null) {
                    //     fallingItems.AddEntity(block.build.effectItem);
                    // }
                }
            });
        });

    }

    public void Move(){
        entityLoader.Move();
        foreach (Block block in this.blocks){
            block.Move();
        }
    }

    public void Render() {
        blocks.RenderEntities();
        boardsLoader.Render();
        entityLoader.Render();
    }

}