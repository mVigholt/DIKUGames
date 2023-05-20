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
/// </summary>
public class EffectItemFactory {

    private readonly Vec2F STD_EXTENT = new Vec2F(0.05f, 0.05f);
    private Random random = new Random();
    private bool _isTimedLevel;
    private Dictionary<EffectItemType, IEffectItemConfig> _powerUpConfigs;
    private Dictionary<EffectItemType, IEffectItemConfig> _hazardConfigs;

    public EffectItemFactory(bool isTimedLevel) {
        _isTimedLevel = isTimedLevel;
        string powerUpsNS = "Breakout.Entities.EffectItems.ItemConfigs.PowerUps";
        string hazardsNS = "Breakout.Entities.EffectItems.ItemConfigs.Hazards";
        _powerUpConfigs = ConfigsInNamespace(powerUpsNS);
        _hazardConfigs = ConfigsInNamespace(hazardsNS);
    }

    private Dictionary<EffectItemType, IEffectItemConfig> ConfigsInNamespace(string nameSpace) {
        var configs = new Dictionary<EffectItemType, IEffectItemConfig>();
        var loader = new EffectItemConfigLoader(nameSpace);
        return loader.CreateMapping();
    }

    /// <summary>Get a random power-up placed at a given position</summary>
    public EffectItem RandomPowerUp(Vec2F pos) {
        return RandomEffectItem(pos, _powerUpConfigs);
    }

    /// <summary>Get a random hazard placed at a given position</summary>
    public EffectItem RandomHazard(Vec2F pos) {
        return RandomEffectItem(pos, _hazardConfigs);
    }

    /// <summary>
    /// Get a random EffectItem based on a config
    // from the supplied dictionary
    /// </summary>
    private EffectItem RandomEffectItem(
        Vec2F pos,
        Dictionary<EffectItemType, IEffectItemConfig> configs
    ) {
        if (configs.Count == 0) {
            throw new Exception("This factory has not been configured");
        }
        int index = random.Next(configs.Count);
        EffectItemType randomType = configs.Keys.ToList()[index];
        IEffectItemConfig config = configs[randomType];
        if (config.IsTimed) {
            return CreateTimedEffectItem(config, pos);
        }
        return CreateInstantEffectItem(config, pos);
    }

    private bool ConfigurationIsValid() {
        // Make life easier for the developers.
        // 
        // - Do all the class names in ItemConfigs
        //   end with "Config"?
        // - Do none of the classes in ItemConfigs
        //   have the same Type?
        // - Do none of the classes in ItemConfigs
        //   have the same IconFileName?
        return true;
    }

    private InstantEffectItem CreateInstantEffectItem(
        IEffectItemConfig config,
        Vec2F pos
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(config.IconFileName);
        GameEvent ev = CreateEvent(config.Type.ToString());
        return new InstantEffectItem(config.Type, shape, image, ev);
    }

    private TimedEffectItem CreateTimedEffectItem(
        IEffectItemConfig config,
        Vec2F pos
    ) {
        DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
        Image image = Assets.LoadImage(config.IconFileName);
        GameEvent activationEvent = CreateEvent(config.Type.ToString());
        EffectItemType deactivationType;
        Enum.TryParse<EffectItemType>(activationEvent.Message, out deactivationType);
        GameEvent deactivationEvent = CreateEvent(deactivationType.ToString() + "Deactivate");
        TimePeriod duration = TimePeriod.NewSeconds(5);
        return new TimedEffectItem(
            config.Type, shape, image, activationEvent, deactivationEvent, duration
        );
    }

    private GameEvent CreateEvent(string msg) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(msg)
            .Build();
    }
}