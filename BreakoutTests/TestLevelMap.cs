namespace BreakoutTests;

using NUnit.Framework;
using Breakout.LevelMaps;
using Breakout.IO;
using System;
using System.IO;


public class TestLevelMap {

    [Test]
    public void TestLevelsCanBeLoaded() {
        Console.WriteLine(Directory.GetCurrentDirectory());
        string[] fileNames = Directory.GetFiles(PathFinder.Levels());
        foreach (string f in fileNames) {
            Console.WriteLine(f);
        }
        Assert.Pass();
    }
}