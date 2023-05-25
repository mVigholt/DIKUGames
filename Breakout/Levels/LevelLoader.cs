namespace Breakout.Levels;

using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;
using Breakout.IO;
using Breakout.Entities.EffectItems;
using Breakout.Graphics;


public class LevelLoader {
    private LoadFile loadFile;

    public double? levelTime {
        get;
        private set;
    }
    public string levelName {
        get;
        private set;
    }

    private int rows;
    private int columns;
    private EffectItemFactory eiFactory;
    public EntityContainer<Block> blocks {
        get; private set;
    }

    public LevelLoader(string fileName) {
        bool isTimedLevel = true;
        eiFactory = new EffectItemFactory(isTimedLevel);
        loadFile = new LoadFile(fileName);
        CreateMap();
    }

    private void CreateMap(){
        var bricks = loadFile.GetMap();
        var meta = loadFile.GetMetaDict();
        var legends = loadFile.GetLegendDict();
        if (meta.ContainsKey("time")){
            levelTime =double.Parse(meta["time"]);
        }
        else{
            levelTime = null;
        }
        if (meta.ContainsKey("name")){
            levelName = meta["name"];
        }
        else{
            levelName = null;
        }
        rows = bricks.Length;
        columns = bricks[0].Length;
        float xExtent = 1.0f / columns;
        float yExtent = 0.9f / rows;
        blocks = new EntityContainer<Block>(rows * columns);
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                string symbol = bricks[r][c].ToString();
                if (symbol != "-") {
                    string imgFileName = legends[symbol];
                    Vec2F pos = new Vec2F(c * xExtent, 0.9f - r * yExtent);
                    //var property = meta.GetValueOrDefault(symbol, "");
                    Block block = BuildBlock(imgFileName, pos, symbol);//property);
                    if (block != null) {
                        blocks.AddEntity(block);
                    }
                }
            }
        }
    }

    private Block BuildBlock(string imgFileName, Vec2F pos, string property) {
        string[] filenameParts = imgFileName.Split('.');
        string baseName = filenameParts[0];
        string fileExt = filenameParts[1];
        string alterImgFileName = $"{baseName}-damaged.{fileExt}";

        IBaseImage image;
        IBaseImage alterImage;
        try {
            image = Assets.LoadImage(imgFileName);
            alterImage = Assets.LoadImage(alterImgFileName);
        } catch (Exception) {
            // If the image cannot be loaded, simply
            // don't create this entity.
            // The game is more fun without a few entities
            // than if it just crashes.
            return null;
        }
        var builder = new Block.Builder()
            .WithPosition(pos)
            .WithValue(1)
            .WithIsHardened(loadFile.MetaContains("hardened", property))
            .WithIsUnbreakable(loadFile.MetaContains("unbreakable", property));
        if (loadFile.MetaContains("powerup", property)) {
            EffectItem powerUp = eiFactory.RandomPowerUp(pos);
            builder = builder
                .WithImage(new OverlayImage(image, powerUp.Image))
                .WithAlterImage(new OverlayImage(alterImage, powerUp.Image))
                .WithEffectItem(powerUp);
        } else if (loadFile.MetaContains("hazard", property)) {
            EffectItem hazard = eiFactory.RandomHazard(pos);
            builder = builder
                .WithImage(new OverlayImage(image, hazard.Image))
                .WithAlterImage(new OverlayImage(alterImage, hazard.Image))
                .WithEffectItem(hazard);
        } else {
            builder = builder
                .WithImage(image)
                .WithAlterImage(alterImage);
        }
        return builder.Build();
    }
}