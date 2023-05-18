namespace Breakout.Entities.Effects;


using System;
using System.Collections.Generic;
using System.Linq;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.IO;
using Breakout.Events;


/// <summary>
/// Create EffectItems (power-ups and hazards).
/// To use this factory, you should configure it
/// to include the power-ups and hazards you want
/// in your level.
/// To do that, add their creation methods
/// to the factory like so:
/// 
///     var factory = new EffectItemFactory();
///     factory.AddPowerUp(factory.Wide);
///     factory.AddPowerUp(factory.ExtraLife);
/// 
/// When you want a random power-up, you can
/// get one from the collection by calling
/// 
///     factory.RandomPowerUp(pos)
///
/// where pos is a position, typically of a Block.
/// </summary>
public class EffectItemFactory {

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

    /// <summary>
    /// Add a factory method to the collection of available power-up
    /// creation methods.
    /// </summary>
    /// <param name="creationMethod">
    /// A factory method that creates an EffectItem based on a position,
    /// such as effectItemFactory.Wide
    /// </param>
    public void AddPowerUp(Func<Vec2F, EffectItem> creationMethod) {
        AddEffectItem(creationMethod, _powerUpCreators);
    }

    /// <summary>
    /// Add a factory method to the collection of available power-up
    /// creation methods.
    /// </summary>
    public void AddHazard(Func<Vec2F, EffectItem> creationMethod) {
        AddEffectItem(creationMethod, _hazardCreators);
    }

    private void AddEffectItem(
        Func<Vec2F, EffectItem> creationMethod,
        Dictionary<EffectItemType, Func<Vec2F, EffectItem>> creators
    ) {
        Vec2F arbitraryPos = new Vec2F(-1f, -1f);
        EffectItem temp = creationMethod(arbitraryPos);
        EffectItemType type = temp.Type;
        creators[temp.Type] = creationMethod;
    }

    /// <summary>Get a random power-up placed at a given position</summary>
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

    /// <summary>Get a random hazard placed at a given position</summary>
    public EffectItem RandomHazard(Vec2F pos) {
        if (_hazardCreators.Count == 0) {
            throw new Exception(
                "Cannot choose a random hazard.\n" +
                "Use EffectItemFactory.AddHazard to add more available hazards " +
                "for the factory to choose from. You have 0."
            );
        }
        int index = random.Next(_hazardCreators.Count);
        EffectItemType randomType = _hazardCreators.Keys.ToList()[index];
        EffectItem hazard = _hazardCreators[randomType](pos);
        return hazard;
    }

    /// <summary>
    /// Power-up: Get some extra points.
    /// This was added for testing purposes, because it is
    /// easy to test.
    /// </summary>
    public InstantEffectItem ExtraPoints(Vec2F pos) {
        return CreateInstantEffectItem(
            pos, "heart_filled.png", EffectItemType.ExtraPoints
        );
    }


    /// <summary>
    /// Power-up: The shuttle gets wider for a time duration
    /// </summary>
    public TimedEffectItem Wide(Vec2F pos) {
        return CreateTimedEffectItem(
            pos, "WidePowerUp.png", EffectItemType.Wide
        );
    }

    public InstantEffectItem ExtraBalls(Vec2F pos) {
        return CreateInstantEffectItem(
            pos, "ExtraBallPowerUp.png", EffectItemType.ExtraBalls
        );
    }

    /// <summary>
    /// Hazard: The shuttle's movement speed increases for a while
    /// </summary>
    public TimedEffectItem SlowDown(Vec2F pos) {
        return CreateTimedEffectItem(
            pos, "Slowness.png", EffectItemType.SlowDown
        );
    }

    private InstantEffectItem CreateInstantEffectItem(
        Vec2F pos,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent ev = CreateEvent(type.ToString());
        return new InstantEffectItem(type, shape, image, ev);
    }

    private TimedEffectItem CreateTimedEffectItem(
        Vec2F pos,
        string imageFilename,
        EffectItemType type
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(imageFilename);
        GameEvent activationEvent = CreateEvent(type.ToString());
        EffectItemType deactivationType;
        Enum.TryParse<EffectItemType>(activationEvent.Message, out deactivationType);
        GameEvent deactivationEvent = CreateEvent(deactivationType.ToString() + "Deactivate");
        TimePeriod duration = TimePeriod.NewSeconds(5);
        return new TimedEffectItem(
            type, shape, image, activationEvent, deactivationEvent, duration
        );
    }

    private GameEvent CreateEvent(string msg) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(msg)
            .Build();
    }
}