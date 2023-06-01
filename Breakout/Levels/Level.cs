namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.Board;

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
    }

    private void LoadBlocks(){
        blocks = levelHandler.blocks;
    }

    private void LoadBoards() {
        boardsLoader = new BoardsLoader(this.levelTime, this.levelName);
        this.countDownBoard = boardsLoader.countDownBoard;
        this.levelBoard = boardsLoader.levelBoard;
    }

    public void Render() {
        blocks.RenderEntities();
        boardsLoader.Render();
    }

}