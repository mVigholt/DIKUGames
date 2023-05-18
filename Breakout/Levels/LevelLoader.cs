namespace Breakout.Levels;

using System;
using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Entities;
using Breakout.IO;
using Breakout.Entities.Effects;
using Breakout.Graphics;


public class LevelLoader {
    private Dictionary<string, string> legends;
    private Dictionary<string, string> metadata;
    private LoadFile loadFile;
    private string[] bricks;
    private int rows;
    private int columns;
    private EffectItemFactory eiFactory = new EffectItemFactory();
    public EntityContainer<Block> blocks{get; private set;}

    private void AddItemsForLevel() {
        // :: Imagine this method taking a level as an input
        // and adding the available powerups and hazards
        // that a level can have to the factory.
        // Some levels don't have the EffectItems that
        // have to do with time.
        // For now this is just hardcoded, so we can easily
        // change it, once we know more.
        eiFactory.AddPowerUp(eiFactory.ExtraPoints);
        eiFactory.AddPowerUp(eiFactory.Wide);
    }

    public LevelLoader(string fileName) {
        AddItemsForLevel();

        loadFile = new LoadFile(fileName);
        legends = loadFile.CreateLegends();
        metadata = loadFile.CreateMetadata();
        bricks = loadFile.CreateMap();
        rows = bricks.Length;
        columns = bricks[0].Length;
        float xExtent = 1.0f / columns;
        float yExtent = 1.0f / rows;
        blocks = new EntityContainer<Block>(rows * columns);
        for (int r = 0; r < bricks.Length; r++) {
            for (int c = 0; c < bricks[r].Length; c++) {
                string symbol = bricks[r][c].ToString();
                if (symbol != "-") {
                    string imgFileName = legends[symbol];
                    string[] filenameParts = imgFileName.Split('.');
                    string baseName = filenameParts[0];
                    string fileExt = filenameParts[1];
                    string alterImgFileName = $"{baseName}-damaged.{fileExt}";
                    
                    string overlayFilename = "BigPowerUp.png";
                    OverlayImage overlayImage = new OverlayImage(imgFileName, overlayFilename);
                    OverlayImage overlayAltImage = new OverlayImage(alterImgFileName, overlayFilename);
                    

                    Image image;
                    try {
                        image = Assets.LoadImage(imgFileName);
                    } catch (Exception) {
                        // If the image cannot be loaded, simply
                        // don't create this entity.
                        // The game is more fun without a few entities
                        // than if it just crashes.
                        continue;
                    }
                    // :: My powerup would be added here
                    Vec2F pos = new Vec2F(c * xExtent, 1 - r * yExtent);
                    var builder = new Block.Builder()
                            .WithImage(overlayImage)
                            .WithAlterImage(overlayAltImage)
                            .WithPosition(pos)
                            .WithEffectItem(eiFactory.RandomPowerUp(pos))
                            .WithValue(1);
                    var property = metadata
                        .GetValueOrDefault(symbol, "");
                    if (property == "hardened") {
                        builder.WithIsHardened(true);
                    }
                    if (property == "unbreakable") {
                        builder.WithIsUnbreakable(true);
                    }
                    blocks.AddEntity(
                        builder.Build()
                    );
                }
            }
        }
    }
}