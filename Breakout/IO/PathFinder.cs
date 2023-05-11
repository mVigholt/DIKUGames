namespace Breakout.IO;

using System;
using System.IO;


/// <summary>
/// A utility for finding paths in a file system.
/// to a specific directory or file.
/// It allows for less headaches when working
/// in either Breakout/ or BreakoutTests/.
/// </summary>
public static class PathFinder {

    /// <summary>
    /// Get the absolute path to DIKUGames, the root of our application.
    /// </summary>
    public static string Root() {
        // The root of our project is located 4 layers
        // above the base directory, which is a build directory
        int generations = 4;
        return UpNLevels(BaseDir(), generations);
    }

    public static string BaseDir() {
        string path = AppDomain.CurrentDomain.BaseDirectory;
        string noTrailingSlash = path.Substring(0, path.Length - 1);
        return noTrailingSlash;
    }

    /// <summary>
    /// Shorten a path by removing one layer from the right.
    /// </summary>
    /// <param name="path">A path in the form of a string</param>
    /// <param name="n">The number of levels to chop off the path</param>
    /// <returns>
    /// A new path, i.e. 
    /// UpNLevels("/a/b/c/d", 2) -> "/a/b"
    /// </returns>
    public static string UpNLevels(string path, int n) {
        if (n <= 0) {
            return path;
        }
        var (parentPath, _) = SplitPath(path);
        return UpNLevels(parentPath, n - 1);
    }

    public static string Assets() {
        // Hardcoded for now
        return Path.Combine(Root(), "Breakout", "Assets");
    }

    public static string Levels() {
        return Path.Combine(Assets(), "Levels");
    }

    public static string Images() {
        return Path.Combine(Assets(), "Images");
    }

    /// <summary>Find the path to a directory</summary>
    public static string Find(string dirName) {
        return PathToDir(Directory.GetCurrentDirectory(), dirName);
    }

    /// <summary>
    /// Recursively cd one directory up, until a certain
    /// directory has been reached.
    /// </summary>
    /// <param name="path">Starting path</param>
    /// <param name="dirName">The directory you want to climb up to</param>
    /// <returns>The path to the directory</returns>
    private static string PathToDir(string path, string dirName) {
        if (path == "" || !path.Contains(dirName)) {
            throw new ArgumentException($"Could not find \"{dirName}\"");
        }
        var (firstPart, lastPart) = SplitPath(path);
        if (lastPart == dirName) {
            return path;
        }
        return PathToDir(firstPart, dirName);
    }

    /// <summary>
    /// Split a path on the last "/", i.e.:
    ///     "/path/to/dir" -> ("/path/to", "dir")
    /// </summary>
    /// <param name="path">The path you want to split</param>
    /// <returns>A shorter path and a directory or file name</returns>
    private static Tuple<string, string> SplitPath(string path) {
        char delimiterMacLinux = '/';
        char delimiterWindows = '\\';
        int index = path.LastIndexOf(delimiterMacLinux);
        bool usingWindows = index == -1;
        if (usingWindows) {
            index = path.LastIndexOf(delimiterWindows);
        }
        string subPath = path.Substring(0, index);
        string dir = path.Substring(index + 1);
        return Tuple.Create(subPath, dir);
    }
}