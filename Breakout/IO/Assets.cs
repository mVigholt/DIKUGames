namespace Breakout.IO;

using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;

///<summary>Cache all the images we are going to use as static varaibles</summary>
public static class Assets {

    private static Dictionary<string, Image> cache;
    public static Image MainMenuImage;
    public static Image BackgroundImage;
    public static Image LifeImage;

    static Assets() {
        cache = new Dictionary<string, Image>();
        MainMenuImage = LoadImage("BreakoutTitleScreen.png");
        BackgroundImage = LoadImage("SpaceBackground.png");
        LifeImage = LoadImage("heart_filled.png");
    }

    /// <summary>
    /// Load an image, i.e.:
    ///     LoadImage("player.png")
    /// </summary>
    public static Image LoadImage(string fileName) {
        if (cache.ContainsKey(fileName)) {
            return cache[fileName];
        }
        Image image = new Image(
            Path.Combine(PathFinder.Images(), fileName)
        );
        cache[fileName] = image;
        return image;
    }
}