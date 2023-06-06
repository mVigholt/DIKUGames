namespace Breakout.IO;

using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;

///<summary>Cache all the images we are going to use as static varaibles</summary>
public static class Assets {

    private static Dictionary<string, Image> cache;
    public static IBaseImage MainMenuImage;
    public static IBaseImage BackgroundImage;
    public static IBaseImage LifeImage;

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
    public static IBaseImage LoadImage(string fileName) {
        try {
            if (cache.ContainsKey(fileName)) {
                return cache[fileName];
            }
            Image image = new Image(
                Path.Combine(PathFinder.Images(), fileName)
            );
            cache[fileName] = image;
            return image;
        } catch (FileNotFoundException) {
            // If the File name is not existing, an NoImage istance will
            // be created. 
            // The game is more fun without a few entities
            // than if it just crashes.
            return new NoImage();
        }

    }
}