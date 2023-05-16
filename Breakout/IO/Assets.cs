namespace Breakout.IO;

using System.IO;
using DIKUArcade.Graphics;

///<summary>Cache all the images we are going to use as static varaibles</summary>
public static class Assets {

    /// <summary>
    /// Load an image, i.e.:
    ///     LoadImage("player.png")
    /// </summary>
    public static Image LoadImage(string fileName) {
        return new Image(
            Path.Combine(PathFinder.Images(), fileName));
    }

    public static Image playerImage = LoadImage("player.png");
    public static Image ball = LoadImage("ball.png");
    public static Image mainMenuImage = LoadImage("BreakoutTitleScreen.png");
    public static Image backGroundImage = LoadImage("SpaceBackground.png");
}