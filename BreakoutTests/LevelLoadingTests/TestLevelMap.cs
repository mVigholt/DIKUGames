namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using Breakout.Levels;
using Breakout.IO;
using System;
using System.IO;


public class TestLevel {

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
            new Level(fileName).CreateMap();
        }
        Assert.Pass();
    }

    [Test]
    public void TestMetaData() {
        
    }
}