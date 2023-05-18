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
    private Dictionary<EffectItemType, Func<Vec2F, EffectItem>> _powerUpCreators;
    private Dictionary<EffectItemType, Func<Vec2F, EffectItem>> _hazardCreators;

    public EffectItemFactory() {
        _powerUpCreators = new Dictionary<EffectItemType, Func<Vec2F, EffectItem>>();
        _hazardCreators = new Dictionary<EffectItemType, Func<Vec2F, EffectItem>>();
    }

    private GameEvent CreateEvent(EffectItemType type) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(type.ToString())
            .Build();
    }

    public void AddPowerUp(Func<Vec2F, EffectItem> creationMethod) {
        AddEffectItem(creationMethod, _powerUpCreators);
    }

    public void AddHazard(Func<Vec2F, EffectItem> creationMethod) {
        AddEffectItem(creationMethod, _hazardCreators);
    }

    private void AddEffectItem(
        Func<Vec2F, EffectItem> creationMethod,
        Dictionary<EffectItemType, Func<Vec2F, EffectItem>> creators
    ) {
        EffectItem temp = creationMethod(new Vec2F(-1f, -1f));
        EffectItemType type = temp.Type;
        creators[temp.Type] = creationMethod;
    }

    public EffectItem RandomPowerUp(Vec2F pos) {
        if (_powerUpCreators.Count == 0) {
            throw new Exception(
                "Cannot choose a random power-up.\n" +
                "Use EffectItemFactory.AddPowerUp to add more available powerups " +
                "for the factory to choose from. You have 0."
            );
        }
        int index = random.Next(_powerUpCreators.Count);
        EffectItemType randomType = _powerUpCreators.Keys.ToList()[index];
        EffectItem powerUp = _powerUpCreators[randomType](pos);
        return powerUp;
    }

    public void RandomHazard() {}

    private InstantEffectItem CreateInstantEffectItem(
        Vec2F pos,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent ev = CreateEvent(type);
        return new InstantEffectItem(type, shape, image, ev);
    }

    private TimedEffectItem CreateTimedEffectItem(
        Vec2F pos,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent activationEvent = CreateEvent(type);
        EffectItemType deactivationType;
        Enum.TryParse<EffectItemType>(activationEvent.Message + "Deactivate", out deactivationType);
        GameEvent deactivationEvent = CreateEvent(deactivationType);
        return new TimedEffectItem(type, shape, image, activationEvent, deactivationEvent, 5000);
    }

    public InstantEffectItem ExtraPoints(Vec2F pos) {
        return CreateInstantEffectItem(
            pos, "heart_filled.png", EffectItemType.ExtraPoints
        );
    }

    public TimedEffectItem Wide(Vec2F pos) {
        return CreateTimedEffectItem(
            pos, "heart_empty.png", EffectItemType.Wide
        );   
    }
}