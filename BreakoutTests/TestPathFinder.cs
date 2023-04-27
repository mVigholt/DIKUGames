namespace BreakoutTests;

using NUnit.Framework;
using Breakout.IO;
using System;


public class TestPathFinder {

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
        string bin = "bin";
        string path = PathFinder.Find(bin);
        string assetsPath = PathFinder.Assets();
        string images = PathFinder.Images();
        string levels = PathFinder.Levels();
        Assert.That(IsCorrectPathToDir(path, bin));
        Assert.That(IsCorrectPathToDir(assetsPath, "Assets"));
        Assert.That(IsCorrectPathToDir(images, "Images"));
        Assert.That(IsCorrectPathToDir(levels, "Levels"));
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