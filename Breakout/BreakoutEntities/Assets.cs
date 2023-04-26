namespace Breakout.BreakoutEntities;

using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;

///<summary>Initialize all the images we are going to use as static varaibles</summary>
public static class Assets {
    private static string[] createFullPath(string asset) {
        List<string> path = new List<string>{"..", "Breakout", "Assets", "Images"};
        path.Add(asset);
        return path.ToArray();
    }
    public static Image playerImage =
        new Image(
            Path.Combine(createFullPath("player.png"))
            );
    public static Image ball =
        new Image(
            Path.Combine(createFullPath("ball.png"))
            );

    public static Image mainMenuImage =
        new Image(
            Path.Combine(createFullPath("BreakoutTitleScreen.png"))
            );
    public static Image backGroundImage =
        new Image(
            Path.Combine(createFullPath("SpaceBackground.png"))
            );
}