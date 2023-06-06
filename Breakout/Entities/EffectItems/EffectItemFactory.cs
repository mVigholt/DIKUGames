namespace Breakout.Entities.EffectItems;

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
using Breakout.Entities.EffectItems.ItemConfigs;


/// <summary>
/// Creates EffectItems, the entities associated with
/// power-ups and hazards.
/// Define an power-up by creating an implementation
/// of IEffectItemConfig and placing it in
/// Breakout.Entities.EffectItems.ItemConfigs.PowerUps.
/// Similarly, you can create a hazard by placing it in
/// Breakout.Entities.EffectItems.ItemConfigs.Hazards.
/// Be sure to give your implementation a class name
/// that ends with "Config".
/// </summary>
public class EffectItemFactory {

    private readonly Vec2F STD_EXTENT = new Vec2F(0.05f, 0.05f);
    private Random random = new Random();
    private Dictionary<string, IEffectItemConfig> _powerUpConfigs;
    private Dictionary<string, IEffectItemConfig> _hazardConfigs;
    private TypeLoader<IEffectItemConfig> _effectLoader;

    public EffectItemFactory() {
        string powerUpsNS = "Breakout.Entities.EffectItems.ItemConfigs.PowerUps";
        string hazardsNS = "Breakout.Entities.EffectItems.ItemConfigs.Hazards";
        _powerUpConfigs = ConfigsInNamespace(powerUpsNS);
        _hazardConfigs = ConfigsInNamespace(hazardsNS);
    }

    /// <summary>Get a random power-up placed at a given position</summary>
    public EffectItem RandomPowerUp(Vec2F pos) {
        return RandomEffectItem(pos, _powerUpConfigs);
    }

    /// <summary>Get a random hazard placed at a given position</summary>
    public EffectItem RandomHazard(Vec2F pos) {
        return RandomEffectItem(pos, _hazardConfigs);
    }

    private Dictionary<string, IEffectItemConfig> ConfigsInNamespace(string nameSpace) {
        _effectLoader = new EffectItemConfigLoader(nameSpace);
        return _effectLoader.CreateMapping();
    }

    /// <summary>
    /// Get a random EffectItem based on a config
    // from the supplied dictionary
    /// </summary>
    private EffectItem RandomEffectItem(
        Vec2F pos,
        Dictionary<string, IEffectItemConfig> configs
    ) {
        if (configs.Count == 0) {
            throw new Exception("This factory has not been configured");
        }
        int index = random.Next(configs.Count);
        string randomType = configs.Keys.ToList()[index];
        IEffectItemConfig config = configs[randomType];
        if (config.IsTimed) {
            return CreateTimedEffectItem(config, pos);
        }
        return CreateInstantEffectItem(config, pos);
    }

    private InstantEffectItem CreateInstantEffectItem(
        IEffectItemConfig config,
        Vec2F pos
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        IBaseImage image = Assets.LoadImage(config.IconFileName);
        GameEvent ev = CreateActivationEvent(config);
        return new InstantEffectItem(shape, image, ev);
    }

    private TimedEffectItem CreateTimedEffectItem(
        IEffectItemConfig config,
        Vec2F pos
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        IBaseImage image = Assets.LoadImage(config.IconFileName);
        GameEvent activationEvent = CreateActivationEvent(config);
        GameEvent deactivationEvent = CreateDeactivationEvent(config);
        TimePeriod duration = TimePeriod.NewSeconds(5);
        return new TimedEffectItem(
            shape, image, activationEvent, deactivationEvent, duration
        );
    }

    private GameEvent CreateActivationEvent(IEffectItemConfig config) {
        string fk = _effectLoader.GetForeignKey(config);
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(fk)
            .Build();
    }

    private GameEvent CreateDeactivationEvent(IEffectItemConfig config) {
        string fk = _effectLoader.GetForeignKey(config);
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(fk + "Deactivate")
            .Build();
    }
}