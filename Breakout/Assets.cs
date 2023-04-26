namespace Breakout;

using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;

///<summary>Initialize all the images we are going to use as static varaibles</summary>
public static class Assets{
    public static Image playerImage =
        new Image(
            Path.Combine("..", "Breakout", "Assets", "Images", "Player.png"));
            
    public static Image mainMenuImage =
        new Image(
            Path.Combine("..", "Breakout", "Assets", "Images", "SpaceBackground.png"));

    public static Image backGroundImage =
        new Image(
            Path.Combine("..", "Breakout", "Assets", "Images", "SpaceBackground.png"));
}