namespace Breakout.Levels;

using Breakout.Entities.Board;

public class BoardsLoader {
    public CountDownBoard CountDownBoard {
        get;
        private set;
    }
    public LevelBoard LevelBoard {
        get;
        private set;
    }
    public double? LevelTime {
        get;
    }
    public string LevelName {
        get;
    }

    public BoardsLoader(double? levelTime, string levelName) {
        this.LevelTime = levelTime;
        this.LevelName = levelName;
        InitCountDown(levelTime);
        InitLevelBoard(levelName);
    }

    private void InitCountDown(double? levelTime) {
        if (levelTime != null) {
            this.CountDownBoard = new CountDownBoard((int) levelTime);
        } else {
            this.CountDownBoard = null;
        }
    }

    private void InitLevelBoard(string levelName) {
        if (levelName != null) {
            this.LevelBoard = new LevelBoard((string) levelName);
        } else {
            this.LevelBoard = null;
        }
    }

    public void Render() {
        if (CountDownBoard != null) {
            CountDownBoard.Render();
        }
        if (LevelBoard != null) {
            LevelBoard.Render();
        }
    }

}