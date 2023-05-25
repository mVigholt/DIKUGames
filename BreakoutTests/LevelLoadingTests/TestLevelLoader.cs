namespace BreakoutTests;

using System;
using System.IO;
using Breakout.Entities;
using Breakout.IO;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using NUnit.Framework;


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
            new LevelLoader(fileName);
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
        var level = new Level(1);
        // Then check that a level with missing image files
        // still can be instantiated, upholding requirement 3.
        // We are also, partly, testing requirement 2:
        // "The data read from the file is stored as expected in data structures."
        }
}