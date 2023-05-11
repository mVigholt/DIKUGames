namespace BreakoutTests;

using System;
using System.IO;
using Breakout.IO;
using NUnit.Framework;


// Make sure that no tests contain the word "DIKUGames",
// as our TA's root folder probably has a different name
// such as "DIKUGames (5)".
public class TestPathFinder {

    /// <summary>
    /// True if the path has no trailing slash and
    /// the directory name is the last part of the path.
    /// </summary>
    private bool IsCorrectPathToDir(string path, string dir) {
        return path.Split(dir)[^1] == "";
    }

    [Test]
    public void TestRoot() {
        string root = PathFinder.Root();
        string breakoutPath = Path.Combine(root, "Breakout");
        Assert.That(Directory.Exists(breakoutPath));
        Assert.That(IsCorrectPathToDir(breakoutPath, "Breakout"));
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

    [Test]
    public void TestBaseDir() {
        Assert.That(PathFinder.BaseDir().Contains("bin"));
        Assert.AreEqual(
            PathFinder.UpNLevels(PathFinder.BaseDir(), 4),
            PathFinder.Root()
        );
    }
}