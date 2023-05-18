namespace Breakout.Entities.Effects;


using System;
using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.IO;
using Breakout.Events;


public class EffectItemFactory {
    // It is not clear what this factory should produce

    private readonly int STD_DURATION = 5000;
    private readonly Vec2F STD_EXTENT = new Vec2F(0.05f, 0.05f);
    private Random random = new Random();
    // private Dictionary<EffectItemType, EffectItem> _availableItems;
    private Dictionary<EffectItemType, EffectItem> _powerUps;
    private Dictionary<EffectItemType, EffectItem> _hazards;

    public EffectItemFactory() {
        _powerUps = new Dictionary<EffectItemType, EffectItem>();
        _hazards = new Dictionary<EffectItemType, EffectItem>();
    }

    private GameEvent CreateEvent(EffectItemType type) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(type.ToString())
            .Build();
    }

    public void AddPowerUp(EffectItem powerUp) {
        _powerUps[powerUp.Type] = powerUp;
    }

    public void AddHazard(EffectItem hazard) {
        _hazards[hazard.Type] = hazard;
    }

    public EffectItem RandomPowerUp(Vec2F pos) {
        if (_powerUps.Count == 0) {
            throw new Exception(
                "Cannot choose a random power-up.\n" +
                "Use EffectItemFactory.AddPowerUp to add more available powerups " +
                "for the factory to choose from. You have 0."
            );
        }
        int index = random.Next(_powerUps.Count);
        EffectItemType randomType = _powerUps.Keys.ToList()[index];
        Console.WriteLine("Randomly chose " + _powerUps[randomType].Type + ", " + _powerUps[randomType].ActivationEvent.Message);
        return _powerUps[randomType];
    }

    public void RandomHazard() {}

    private InstantEffectItem CreateInstantEffectItem(
        Vec2F position,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(position, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent ev = CreateEvent(type);
        return new InstantEffectItem(type, shape, image, ev);
    }

    private TimedEffectItem CreateTimedEffectItem(
        Vec2F position,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(position, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent activationEvent = CreateEvent(type);
        EffectItemType deactivationType;
        Enum.TryParse<EffectItemType>(activationEvent.Message + "Deactivate", out deactivationType);
        GameEvent deactivationEvent = CreateEvent(deactivationType);
        return new TimedEffectItem(type, shape, image, activationEvent, deactivationEvent, 5000);
    }

    public InstantEffectItem ExtraPoints(Vec2F position) {
        return CreateInstantEffectItem(
            position, "heart_filled.png", EffectItemType.ExtraPoints
        );
        // DynamicShape shape = new DynamicShape(position, STD_EXTENT);
        // Image image = Assets.LoadImage("heart_filled.png");
        // GameEvent ev = CreateEvent(EffectItemType.ExtraPoints);
        // return new InstantEffectItem(EffectItemType.ExtraPoints, shape, image, ev);
    }

    public TimedEffectItem Wide(Vec2F position) {
        return CreateTimedEffectItem(
            position, "heart_empty.png", EffectItemType.Wide
        );
        
    }
}