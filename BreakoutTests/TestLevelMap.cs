namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using Breakout.LevelMaps;
using Breakout.IO;
using System;
using System.IO;


public class TestLevelMap {

    [SetUp]
    public void SetUp() {
        // For linux, we need this line
        Window.CreateOpenGLContext();
    }

    [Test]
    public void TestLevelsCanBeLoaded() {
        Console.WriteLine(Directory.GetCurrentDirectory());
        string[] fileNames = Directory.GetFiles(PathFinder.Levels());
        foreach (string fileName in fileNames) {
            new LevelMap(fileName).CreateMap();
        }
        Assert.Pass();
    }
}