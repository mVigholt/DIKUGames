namespace Breakout.Levels;

using Breakout.Entities.Board;

public class BoardsLoader {
    public CountDownBoard countDownBoard {
        get;
        private set;
    }
    public LevelBoard levelBoard {
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
            this.countDownBoard = new CountDownBoard((int) levelTime);
        } else {
            this.countDownBoard = null;
        }
    }

    private void InitLevelBoard(string levelName) {
        if (levelName != null) {
            this.levelBoard = new LevelBoard((string) levelName);
        } else {
            this.levelBoard = null;
        }
    }

    public void Render() {
        if (countDownBoard != null) {
            countDownBoard.Render();
        }
        if (levelBoard != null) {
            levelBoard.Render();
        }
    }

}