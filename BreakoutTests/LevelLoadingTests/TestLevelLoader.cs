namespace BreakoutTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using DIKUArcade.Entities;
using Breakout.Levels;
using Breakout.IO;
using Breakout.Entities;
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
        string[] fileNames = Directory.GetFiles(PathFinder.Levels());
        foreach (string fileName in fileNames) {
            LevelLoader.Load(fileName);
        }
        Assert.Pass();
        // By loading every level, we assure that it can
        // handle all metadata. By not throwing an exception,
        // we know that requirement 1 is being upheld.
        // We do not have any additional requirements for
        // what those metadata fields should be used for,
        // so for now, this test is sufficient.
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
        // still can be instantiated, upholding requirement 3.
        // We are also, partly, testing requirement 2:
        // "The data read from the file is stored as expected in data structures."
        var containerWithMissingImage = new EntityContainer<Block>();
        containerWithMissingImage = LevelLoader.Load(
            "level_with_missing_files.txt");
        int lowerNumBlocks = 56;
        Assert.AreEqual(
            lowerNumBlocks, containerWithMissingImage.CountEntities());
    }
}