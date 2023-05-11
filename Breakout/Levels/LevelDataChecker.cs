namespace Breakout.Levels;

using System;
using System.IO;
using Breakout.Entities;
using Breakout.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;


public class LevelDataChecker {

    public static bool IsValid(string fileName) {
        return LevelExists(fileName);
        // && more checks here
    }

    private static bool LevelExists(string fileName) {
        try {
            Path.Combine(PathFinder.Levels(), fileName);
            return true;
        } catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}