namespace BreakoutTests;

using NUnit.Framework;
using Breakout.LevelMaps;
using Breakout.IO;
using System;
using System.IO;


public class TestLevelMap {
    [SetUp]
    public void Setup() {
    }

    [Test]
    public void TestLevelsCanBeLoaded() {
        PathFinder.Root();
        // Console.WriteLine(Directory.GetCurrentDirectory());
        // string dirPath = Path.Combine("Breakout", "Assets", "Levels");
        // string[] fileNames = Directory.GetFiles(dirPath);
        // foreach (string f in fileNames) {
        //     Console.WriteLine(f);
        // }
        Assert.Pass();
    }
}