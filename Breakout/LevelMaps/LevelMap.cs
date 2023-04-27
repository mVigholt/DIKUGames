namespace Breakout.LevelMaps;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Breakout.BreakoutEntities;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


public class LevelMap {
    public string TxtFile {
        get; private set;
    }
    public EntityContainer<Block> blocks;
    private int colNum;
    private int rowNum;
    private float xExtend;
    private float yExtend;
    private char[] lineChar;
    private List<char[]> symbolList = new();
    private char[][] brickArray;
    private Dictionary<string, string> legendsDict = new Dictionary<string, string>();
    public LevelMap(string txtFile) {
        this.TxtFile = txtFile;
        this.blocks = new EntityContainer<Block>(24 * 12);
    }
    public void CreateMap() {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(PathFinder.Levels(), TxtFile);
        try {
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
            foreach (string l in legends) {
                string[] le = l.Split(')');
                string icon = le[0];
                string image = le[1].Trim();
                legendsDict.Add(icon, image);
            }

            //convert each line to a char array, and then
            //add the char array of each line to the symbolList.
            foreach (string line in maps) {
                lineChar = line.ToArray();//char array for each line
                symbolList.Add(lineChar);
            }
            // Convert the list of charArray to a 2d array
            brickArray = symbolList.ToArray();
            rowNum = brickArray.Length; // The total row number
            colNum = brickArray[0].Length; // The total column number

            xExtend = 1.0f / colNum;
            yExtend = 1.0f / rowNum;

            this.blocks = new EntityContainer<Block>(rowNum * colNum);
            for (int r = 0; r < brickArray.Length; r++) {
                for (int c = 0; c < brickArray[r].Length; c++) {
                    // Console.WriteLine($"brickArray[{r}, {c}]: " + brickArray[r][c].ToString());
                    if (brickArray[r][c] != '-') {
                        blocks.AddEntity(
                        new Block.Builder()
                            .WithImage(new Image(Path.Combine(PathFinder.Images(), legendsDict[brickArray[r][c].ToString()])))
                            .WithPosition(new Vec2F(c * xExtend, 1 - r * yExtend))
                            .Build()
                        );
                    }
                }
            }

        } catch (FileNotFoundException e) {
            Console.WriteLine(e.Message);
        }

    }
}

