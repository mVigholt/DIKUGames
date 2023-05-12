namespace Breakout.Levels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Breakout.Entities;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public static class LevelLoader {

    public static EntityContainer<Block> Load(string fileName) {
        string filePath = Path.Combine(PathFinder.Levels(), fileName);
        IEnumerable<string> legends = File.ReadLines(filePath)
            .SkipWhile(legend => legend != "Legend:")
            .Skip(1) // Skip the intro line
            .TakeWhile(legend => legend != "Legend/");
        IEnumerable<string> meta = File.ReadLines(filePath)
            .SkipWhile(meta => meta != "Meta:")
            .Skip(1) // Skip the intro line
            .TakeWhile(meta => meta != "Meta/");
        IEnumerable<string> maps = File.ReadLines(filePath)
            .SkipWhile(map => map != "Map:")
            .Skip(1) // Skip the intro line
            .TakeWhile(map => map != "Map/");

        //Create a legends dictionary
        var legendsDict = new Dictionary<string, string>();
        foreach (string l in legends) {
            string[] parts = l.Split(')');
            string symbol = parts[0].Trim();
            string imagePath = parts[1].Trim().ToLower();
            legendsDict.Add(symbol, imagePath);
        }

        var properties = new Dictionary<string, string>();
        foreach (string pair in meta) {
            string[] parts = pair.Split(":");
            string property = parts[0].Trim().ToLower();
            string symbol = parts[1].Trim();
            properties[symbol] = property;
        }

        string[] bricks = maps.ToArray();
        int rows = bricks.Length;
        int columns = bricks[0].Length;
        float xExtent = 1.0f / columns;
        float yExtent = 1.0f / rows;        

        var blocks = new EntityContainer<Block>(rows * columns);        
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                string symbol = bricks[r][c].ToString();
                if (symbol != "-") {
                    string imgFileName = legendsDict[bricks[r][c].ToString()];
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
                    var property = properties
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
        return blocks;
    }
}