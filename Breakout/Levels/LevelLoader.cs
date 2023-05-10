namespace Breakout.Levels;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.IO;

public static class LevelLoader {

    public static EntityContainer<Block> Load(string fileName) {
        string filePath = Path.Combine(PathFinder.Levels(), fileName);
        var legends = File.ReadLines(filePath)
            .SkipWhile(legend => legend != "Legend:")
            .Skip(1) // Skip the intro line
            .TakeWhile(legend => legend != "Legend/");
        var metadata = File.ReadLines(filePath)
            .SkipWhile(meta => meta != "Meta:")
            .Skip(1) // Skip the intro line
            .TakeWhile(meta => meta != "Meta/");
        var maps = File.ReadLines(filePath)
            .SkipWhile(map => map != "Map:")
            .Skip(1) // Skip the intro line
            .TakeWhile(map => map != "Map/");

        //Create a legends dictionary
        var legendsDict = new Dictionary<string, string>();
        foreach (string l in legends) {
            string[] le = l.Split(')');
            string icon = le[0];
            string image = le[1].Trim();
            legendsDict.Add(icon, image);
        }

        //convert each line to a char array, and then
        //add the char array of each line to the symbolList.
        char[] lineChars;
        List<char[]> symbolLists = new();
        foreach (string line in maps) {
            lineChars = line.ToArray();//char array for each line
            symbolLists.Add(lineChars);
        }

        // Convert the list of charArray to a 2d array
        char[][] bricks = symbolLists.ToArray();
        int rows = bricks.Length; // The total row number
        int columns = bricks[0].Length; // The total column number
        float xExtent = 1.0f / columns;
        float yExtent = 1.0f / rows;

        var blocks = new EntityContainer<Block>(rows * columns);
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                if (bricks[r][c] != '-') {
                    string imgFileName = legendsDict[bricks[r][c].ToString()];
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
                    blocks.AddEntity(
                        new Block.Builder()
                            .WithImage(Assets.LoadImage(imgFileName))
                            .WithPosition(new Vec2F(c * xExtent, 1 - r * yExtent))
                            .Build()
                        );
                }
            }
        }
        return blocks;
    }
}