namespace BreakoutTests.LevelLoadingTests;

using System.IO;
using Breakout.IO;
using Breakout.Levels;
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
        //Precondition: FileNotfind.txt is not existing
        string fileName = "FileNotfind.txt";
        LevelDataLoader level = new LevelDataLoader(fileName);
        string[] expectedArray = new string[] { "hhhhhhhhhhh" };
        foreach (var i in level.GetMap()) {
            Assert.That(i, Is.EqualTo("hhhhhhhhhhh"));
        }
    }

    [Test]
    public void TestGetMetaDict() {
        //Precondition: level1.txt is existing
        LevelDataLoader levelLoader = new LevelDataLoader("level1.txt");
        var metaDict = levelLoader.GetMetaDict();
        Assert.That(metaDict, Does.ContainKey("name").WithValue("level 1"));
        Assert.That(metaDict, Does.ContainKey("time").WithValue("300"));
        Assert.That(metaDict, Does.ContainKey("hardened").WithValue("#"));
    }

    [Test]
    public void TestGetLegendDict() {
        //Precondition: level2.txt is existing
        LevelDataLoader levelLoader = new LevelDataLoader("level2.txt");
        var legendDict = levelLoader.GetLegendDict();
        Assert.That(legendDict, Does.ContainKey("h").WithValue("green-block.png"));
        Assert.That(legendDict, Does.ContainKey("i").WithValue("teal-block.png"));
    }

    [Test]
    public void TestMetaContains() {
        //Precondition: level3.txt is existing
        LevelDataLoader levelLoader = new LevelDataLoader("level3.txt");
        Assert.IsTrue(levelLoader.MetaContains("name", "level 3"));
        Assert.IsFalse(levelLoader.MetaContains("NotContain", "ThisValue"));
    }
}