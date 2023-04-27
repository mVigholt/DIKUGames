namespace BreakoutTests;

using NUnit.Framework;
using Breakout.IO;
using System;


public class TestPathFinder {
    [SetUp]
    public void Setup() {
    }

    private bool IsCorrectPathToDir(string path, string dir) {
        return path.Split(dir)[^1] == "";
    }

    [Test]
    public void TestRoot() {
        string pathToRoot = PathFinder.Root();
        string dir = "DIKUGames";
        Assert.That(pathToRoot.Contains(dir));
        Assert.That(IsCorrectPathToDir(pathToRoot, dir));
    }

    [Test]
    public void TestFind() {
        string binaryOutputDir = "bin";
        string path = PathFinder.Find(binaryOutputDir);
        Assert.That(IsCorrectPathToDir(path, binaryOutputDir));
        string nonexistentDirName = "pwkjMx5Zu5SNh";
        Exception exception = Assert.Throws<ArgumentException>(() => {
            PathFinder.Find(nonexistentDirName);
        });
        Assert.AreEqual(
            exception.Message, 
            $"Could not find \"{nonexistentDirName}\""
        );
    }
}