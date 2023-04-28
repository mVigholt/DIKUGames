namespace Breakout.IO;

using System;
using System.IO;


/// <summary>
/// A utility for finding paths in a file system.
/// to a specific directory or file.
/// It allows for less headaches when working
/// in either Breakout/ or BreakoutTests/
/// </summary>
public static class PathFinder {

    /// <summary>
    /// Get the absolute path to DIKUGames, the root of our application.
    /// </summary>
    public static string Root() {
        return Find("DIKUGames");
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
        return GetDirName(Directory.GetCurrentDirectory(), dirName);
    }

    /// <summary>
    /// Recursively cd one directory up, until a certain
    /// directory has been reached.
    /// </summary>
    /// <param name="path">Starting path</param>
    /// <param name="dirName">The directory you want to climb up to</param>
    /// <returns>The path to the directory</returns>
    private static string GetDirName(string path, string dirName) {
        if (path == "") {
            throw new ArgumentException($"Could not find \"{dirName}\"");
        }
        var (firstPart, lastPart) = SplitPath(path);
        if (lastPart == dirName) {
            return path;
        }
        return GetDirName(firstPart, dirName);
    }

    /// <summary>
    /// Split a path on the last "/", i.e.:
    ///     "/path/to/dir" -> ("/path/to", "dir")
    /// </summary>
    /// <param name="path">The path you want to split</param>
    /// <returns>A shorter path and a directory or file name</returns>
    private static Tuple<string, string> SplitPath(string path) {
        Console.WriteLine($"Trying to split the string {path}");
        int index = path.LastIndexOf('/');
        string subPath = path.Substring(0, index);
        string dir = path.Substring(index + 1);
        return Tuple.Create(subPath, dir);
    }
}