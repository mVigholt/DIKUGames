namespace Breakout.Levels;

using System;
using System.Collections.Generic;
using System.Linq;
using Breakout.Entities;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class LevelLoader {
    private Dictionary<string, string> legends;
    private Dictionary<string, string> metadata;
    private LoadFile loadFile;
    private string[] bricks;
    private int rows;
    private int columns;
    public EntityContainer<Block> blocks{get; private set;}
    public LevelLoader(string fileName) {
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
                    string[] filenameParts = imgFileName.Split('.');
                    string baseName = filenameParts[0];
                    string fileExt = filenameParts[1];
                    string alterImgFileName = $"{baseName}-damaged.{fileExt}";
                    Image image;
                    try {
                        image = Assets.LoadImage(imgFileName);
                    } catch (Exception) {
                        // If the image cannot be loaded, simply
                        // don't create this entity.
                        // The game is more fun without a few entities
                        // than if it just crashes.
                        continue;
                    }
                    var builder = new Block.Builder()
                            .WithImage(Assets.LoadImage(imgFileName))
                            .WithAlterImage(Assets.LoadImage(alterImgFileName))
                            .WithPosition(new Vec2F(c * xExtent, 1 - r * yExtent))
                            .WithValue(1);
                    var property = metadata
                        .GetValueOrDefault(symbol, "");
                    if (property == "hardened") {
                        builder.WithIsHardened(true);
                    }
                    if (property == "unbreakable") {
                        builder.WithIsUnbreakable(true);
                    }
                    blocks.AddEntity(
                        builder.Build()
                    );
                }
            }
        }
    }
}