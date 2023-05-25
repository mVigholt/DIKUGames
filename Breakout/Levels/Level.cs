namespace Breakout.Levels;

using System;
using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Events;
using Breakout.Entities.Board;

public class Level : IGameEventProcessor {
    public countDownBoard countDownBoard {
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

    // public EntityContainer<EffectItem> fallingItems;
    // public EffectItemHandler effectItemHandler;

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
            this.countDownBoard = new countDownBoard((int) levelTime);
        } else {
            this.countDownBoard = null;
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
        if (countDownBoard != null) {
            countDownBoard.Render();
        }
        if (levelBoard!= null) {
            levelBoard.Render();
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        throw new NotImplementedException();
    }
}