namespace BreakoutTests.LevelLoadingTests;

using System.IO;
using Breakout.IO;
using Breakout.Levels;
using DIKUArcade.GUI; // Needed for OpenGL contexts
using NUnit.Framework;

[TestFixture]
public class TestLevelDataLoader {

    [SetUp]
    public void SetUp() {
    }


    [Test]
    public void TestLevelsCanBeLoaded() {
        string[] fileNames = Directory.GetFiles(PathFinder.Levels());
        foreach (string fileName in fileNames) {
            new LevelDataLoader(fileName);
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
    public void TestFileNotFind() {
        string fileName = "FileNotfind.txt";
        LevelDataLoader level = new LevelDataLoader(fileName);
        string[] expectedArray = new string[] { "hhhhhhhhhhh" };
        foreach (var i in level.GetMap()) {
            Assert.That(i, Is.EqualTo("hhhhhhhhhhh"));
        }
    }
}