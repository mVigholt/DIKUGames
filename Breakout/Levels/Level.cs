namespace Breakout.Levels;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Entities;
using DIKUArcade.Events;
using Breakout.Entities.Board;

public class Level : IGameEventProcessor {
    public CountDown countDownBoard {
        get; set;
    }
    public LevelBoard levelBoard {
        get; set;
    }
    public LevelLoader levelLoader {
        get; set;
    }
    public EntityContainer<Block> blocks;
    public int LevelNum {
        get; set;
    }
    public int? levelTime {
        get;
        private set;
    }
    public string levelName {
        get;
        private set;
    }

    // public EntityContainer<EffectItem> fallingItems;
    // public EffectItemHandler effectItemHandler;

    public Level(int levelnum) {
        this.LevelNum = levelnum;
        levelLoader = new LevelLoader("level" + (this.LevelNum).ToString() + ".txt");
        blocks = levelLoader.blocks;
        levelTime = levelLoader.levelTime;
        levelName = levelLoader.levelName;
        InitCountDown();
        InitLevelBoard();
    }


    public void InitCountDown() {
        if (levelLoader.levelTime != null) {
            this.countDownBoard = new CountDown((int) levelTime);
        } else {
            this.countDownBoard = null;
        }
    }

    public void InitLevelBoard() {
        if (levelLoader.levelName != null) {
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