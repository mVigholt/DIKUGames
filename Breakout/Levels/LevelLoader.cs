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
    private Dictionary<string, string> legends;
    private Dictionary<string, string> metadata;
    private LoadFile loadFile;
    private string[] bricks;
    private int rows;
    private int columns;
    private EffectItemFactory eiFactory = new EffectItemFactory();
    public EntityContainer<Block> blocks {
        get; private set;
    }

    public LevelLoader(string fileName) {
        bool isTimedLevel = true;
        eiFactory = EffectItemFactory.Create(isTimedLevel);

        loadFile = new LoadFile(fileName);
        legends = loadFile.CreateLegends();
        metadata = loadFile.CreateMetadata();
        bricks = loadFile.CreateMap();
        rows = bricks.Length;
        columns = bricks[0].Length;
        float xExtent = 1.0f / columns;
        float yExtent = 1.0f / rows;
        blocks = new EntityContainer<Block>(rows * columns);
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                string symbol = bricks[r][c].ToString();
                if (symbol != "-") {
                    string imgFileName = legends[symbol];
                    Vec2F pos = new Vec2F(c * xExtent, 1 - r * yExtent);
                    var property = metadata.GetValueOrDefault(symbol, "");
                    Block block = BuildBlock(imgFileName, pos, property);
                    if (block != null) {
                        blocks.AddEntity(block);
                    }
                }
            }
        }
    }

    private Block BuildBlock(
        string imgFileName, Vec2F pos, string property
    ) {
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
            .WithValue(1);
        if (property == "powerup") {
            EffectItem powerUp = eiFactory.RandomPowerUp(pos);
            builder = builder
                .WithImage(new OverlayImage(image, powerUp.Image))
                .WithAlterImage(new OverlayImage(alterImage, powerUp.Image))
                .WithEffectItem(powerUp);
        } else if (property == "hazard") {
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
        if (property == "hardened") {
            builder.WithIsHardened(true);
        } else if (property == "unbreakable") {
            builder.WithIsUnbreakable(true);
        }
        return builder.Build();
    }
}