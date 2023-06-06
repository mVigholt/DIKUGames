namespace Breakout.Levels;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;
using Breakout.IO;
using Breakout.Entities.EffectItems;
using Breakout.Graphics;

/// <summary>
/// Load the map field from the ASCII file, and then
/// create blocks according to the meta data in the ASCII file
/// The blocks stored in a container.
/// </summary>
public class BlockLoader {
    private LevelDataLoader loadFile;
    private EffectItemFactory eiFactory;

    public double? levelTime {
        get; private set;
    }
    public string levelName {
        get; private set;
    }
    public EntityContainer<Block> Blocks {
        get; private set;
    }

    public BlockLoader(string fileName) {
        eiFactory = new EffectItemFactory();
        loadFile = new LevelDataLoader(fileName);
        CreateMap();
    }

    private void CreateMap() {
        var bricks = loadFile.GetMap();
        var meta = loadFile.GetMetaDict();
        var legends = loadFile.GetLegendDict();
        // check if the meta contains time info
        if (meta.ContainsKey("time")) {
            levelTime = double.Parse(meta["time"]);
        } else {
            levelTime = null;
        }
        // check if the meta contains name info
        if (meta.ContainsKey("name")) {
            levelName = meta["name"];
        } else {
            levelName = null;
        }

        int rows = bricks.Length;
        int columns = bricks[0].Length;
        float xExtent = 1.0f / columns; //one block's width
        float yExtent = 0.9f / rows;//one block's hight
        Blocks = new EntityContainer<Block>(rows * columns);
        //Go though each element in the array and create a block and
        //add it to the blocks container
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                string symbol = bricks[r][c].ToString();
                if (symbol != "-") {
                    string imgFileName = legends[symbol];
                    Vec2F pos = new Vec2F(c * xExtent, 0.9f - r * yExtent);
                    Block block = BuildBlocks(imgFileName, pos, symbol);
                    if (block != null) {
                        Blocks.AddEntity(block);
                    }
                }
            }
        }
    }

    /// <summary>
    /// The method is to build one block by using block builder
    /// conbined with meta data.
    /// </summary>
    private Block BuildBlocks(string imgFileName, Vec2F pos, string property) {
        string[] filenameParts = imgFileName.Split('.');
        string baseName = filenameParts[0];
        string fileExt = filenameParts[1];
        string alterImgFileName = $"{baseName}-damaged.{fileExt}";

        IBaseImage image;
        IBaseImage alterImage;
        image = Assets.LoadImage(imgFileName);
        alterImage = Assets.LoadImage(alterImgFileName);
        var builder = new Block.Builder()
            .WithPosition(pos)
            .WithValue(1)
            .WithIsHardened(loadFile.MetaContains("hardened", property))
            .WithIsUnbreakable(loadFile.MetaContains("unbreakable", property))
            .WithMoveable(loadFile.MetaContains("moveable", property));

        if (loadFile.MetaContains("powerup", property)) {
            EffectItem powerUp = eiFactory.RandomPowerUp(pos);
            builder = builder
                .WithImage(new OverlayImage(image, powerUp.Image))
                .WithAlterImage(new OverlayImage(alterImage, powerUp.Image))
                .WithEffectItem(powerUp);
        } else if (loadFile.MetaContains("hazard", property)) {
            EffectItem hazard = eiFactory.RandomHazard(pos);
            builder = builder
                .WithImage(new OverlayImage(image, hazard.Image))
                .WithAlterImage(new OverlayImage(alterImage, hazard.Image))
                .WithEffectItem(hazard);
        } else {
            builder = builder
                .WithImage(image)
                .WithAlterImage(alterImage);
        }
        return builder.Build();
    }
}