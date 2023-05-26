namespace Breakout.Levels;

using System;
using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Events;
using Breakout.Entities.Board;

public class Level {
    public CountDownBoard CountDownBoard {
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
    public EntityContainer<Block> blocks{
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

    public Level(int levelNum) {
        levelHandler = new LevelHandler("level" + (levelNum).ToString() + ".txt");
        blocks = levelHandler.blocks;
        this.levelTime = levelHandler.levelTime;
        this.levelName = levelHandler.levelName;
        InitCountDown();
        InitLevelBoard();
    }


    private void InitCountDown() {
        if (levelHandler.levelTime != null) {
            this.CountDownBoard = new CountDownBoard((int) levelTime);
        } else {
            this.CountDownBoard = null;
        }
    }

    private void InitLevelBoard() {
        if (levelHandler.levelName != null) {
            this.levelBoard = new LevelBoard((string)levelName);
        } else {
            this.levelBoard = null;
        }
    }

    public void Render() {
        blocks.RenderEntities();
        if (CountDownBoard != null) {
            CountDownBoard.Render();
        }
        if (levelBoard!= null) {
            levelBoard.Render();
        }
    }
}