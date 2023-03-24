namespace Galaga;

using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;

public static class Assets{
    public static List<Image> blueEnemyStride =
        ImageStride.CreateStrides(
            4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
    public static List<Image> greenEnemyStride =
        ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));
    public static List<Image> redEnemyStride =
        ImageStride.CreateStrides(
            2, Path.Combine("..", "Galaga", "Assets", "Images", "RedMonster.png"));
    public static List<Image> explosionStrides =
        ImageStride.CreateStrides(
            8, Path.Combine("..", "Galaga", "Assets", "Images", "Explosion.png"));
    public static Image playerImage =
        new Image(
            Path.Combine("..", "Galaga", "Assets", "Images", "Player.png"));
    public static Image playerShotImage =
        new Image(
            Path.Combine("..", "Galaga", "Assets", "Images", "BulletRed2.png"));
    public static Image mainMenuImage =
        new Image(
            Path.Combine("..", "Galaga", "Assets", "Images", "TitleImage.png"));
    public static Image backGroundImage =
        new Image(
            Path.Combine("..", "Galaga", "Assets", "Images", "SpaceBackground.png"));
}