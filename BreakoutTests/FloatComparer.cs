namespace BreakoutTests;

using System;

public static class FloatComparer {

    /// <summary>
    /// Return true if a is almost equal to b
    /// (difference is less than 1 / 1,000,000).
    /// Print a debug message if that is not the case.
    /// </summary>
    public static bool AreAlmostEqual(float a, float b) {
        float max_allowed_diff = 0.000001f;
        float diff = Math.Abs(a - b);
        bool almostEqual = diff < max_allowed_diff;
        if (!almostEqual) {
            Console.WriteLine(
                $"|a - b| < {max_allowed_diff} => \n" +
                $"|{a} - {b}| < {max_allowed_diff} => \n" +
                $"{diff} < {max_allowed_diff} => false");
        }
        return almostEqual;
    }
}