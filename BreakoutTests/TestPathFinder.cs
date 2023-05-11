namespace BreakoutTests;

using System;
using Breakout.IO;
using NUnit.Framework;


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
        Assert.That(
            () => PathFinder.Find(nonexistentDirName),
            Throws.ArgumentException.With.Message.EqualTo(
                $"Could not find \"{nonexistentDirName}\""));
    }

    [Test]
    public void TestFindNonexistentAsset() {
        string fileName = "Tgz7VXx3GL4KM.png";
        Assert.That(
            () => PathFinder.Find(fileName),
            Throws.ArgumentException.With.Message.EqualTo(
                $"Could not find \"{fileName}\""
            )
        );
        Assert.Pass();
    }
}