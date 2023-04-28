namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using DIKUArcade.Entities;
using Breakout.Levels;
using Breakout.IO;
using Breakout.BreakoutEntities;
using System;
using System.IO;


public class Loader {

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
            LevelLoader.Load(fileName);
        }
        Assert.Pass();
    }

    [Test]
    public void TestMissingImage() {
        var container = new EntityContainer<Block>();
        // Test that level 1 can be loaded and has entities,
        // which it should
        container = LevelLoader.Load("level1.txt");
        int numBlocks = 76;
        Assert.AreEqual(numBlocks, container.CountEntities());
        // Then check that a level with missing image files
        // still can be instantiated
        var containerWithMissingImage = new EntityContainer<Block>();
        containerWithMissingImage = LevelLoader.Load(
            "level_with_missing_files.txt");
        int lowerNumBlocks = 56;
        Assert.AreEqual(
            lowerNumBlocks, containerWithMissingImage.CountEntities());
    }
}