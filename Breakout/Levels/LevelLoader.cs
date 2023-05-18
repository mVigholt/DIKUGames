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
        // Imagine this method taking a level as an input
        // and adding the available powerups and hazards
        // that a level can have to the factory.
        // Some levels don't have the EffectItems that
        // have to do with time.
        // For now this is just hardcoded, so we can easily
        // change it, once we know more.
        // eiFactory.AddPowerUp(eiFactory.ExtraPoints);
        eiFactory.AddPowerUp(eiFactory.Wide);
        eiFactory.AddPowerUp(eiFactory.ExtraBalls);
        eiFactory.AddHazard(eiFactory.SlowDown);
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
                    
                    var powerUpImages = new Dictionary<EffectItemType, string>();
                    powerUpImages.Add(EffectItemType.ExtraPoints, "heart_filled.png");
                    powerUpImages.Add(EffectItemType.Wide, "PowerUpWide.png");

                    IBaseImage image;
                    IBaseImage alterImage;
                    Vec2F pos = new Vec2F(c * xExtent, 1 - r * yExtent);
                    
                    EffectItem powerUp = eiFactory.RandomPowerUp(pos);
                    IBaseImage powerUpImage = powerUp.Image;
                    try {
                        image = Assets.LoadImage(imgFileName);
                        alterImage = Assets.LoadImage(alterImgFileName);
                    } catch (Exception) {
                        // If the image cannot be loaded, simply
                        // don't create this entity.
                        // The game is more fun without a few entities
                        // than if it just crashes.
                        continue;
                    }
                    OverlayImage overlayImage = new OverlayImage(image, powerUpImage);
                    OverlayImage overlayAltImage = new OverlayImage(alterImage, powerUpImage);

                    var builder = new Block.Builder()
                            .WithImage(overlayImage)
                            .WithAlterImage(overlayAltImage)
                            .WithPosition(pos)
                            .WithEffectItem(powerUp)
                            .WithValue(1);
                    // builder = MaybeAddEffectItem(builder);
                    var property = metadata
                        .GetValueOrDefault(symbol, "");
                    if (property == "hardened") {
                        builder.WithIsHardened(true);
                    }
                    if (property == "powerup") {
                        Console.WriteLine("POWERUPPPP");
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

    private Block.Builder MaybeAddEffectItem(Block.Builder builder) {
        float randnum = 0.9323f;
        if (randnum < 0.8f) {
            return builder;
        }
        if (randnum < 0.9f) {
            return builder.WithEffectItem(
                eiFactory.RandomPowerUp(builder.position));
        }
        return builder.WithEffectItem(
            eiFactory.RandomHazard(builder.position));
    }
}