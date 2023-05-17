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
    private Random random = new Random();
    // private Dictionary<EffectItemType, EffectItem> _availableItems;
    private Dictionary<EffectItemType, EffectItem> _powerUps;
    private Dictionary<EffectItemType, EffectItem> _hazards;

    public EffectItemFactory() {
        _powerUps = new Dictionary<EffectItemType, EffectItem>();
        _hazards = new Dictionary<EffectItemType, EffectItem>();
    }

    private GameEvent CreateEvent(string message) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(message)
            .Build();
    }

    public void AddPowerUp(EffectItem powerUp) {
        _powerUps[powerUp.Type] = powerUp;
    }

    public void AddHazard(EffectItem hazard) {
        _hazards[hazard.Type] = hazard;
    }

    public EffectItem RandomPowerUp(Vec2F pos) {
        var l  = _powerUps.Keys;
        Console.WriteLine(_powerUps.Count);
        int index = random.Next(_powerUps.Count);
        EffectItemType randomType = _powerUps.Keys.ToList()[index];
        return _powerUps[randomType];
    }

    public void RandomHazard() {}

    public InstantEffectItem ExtraPoints(Vec2F position) {
        Vec2F extent = new Vec2F(0.05f, 0.05f);
        DynamicShape shape = new DynamicShape(position, extent);
        Image image = Assets.LoadImage("heart_filled.png");
        GameEvent ev = CreateEvent(EffectItemType.ExtraPoints.ToString());
        return new InstantEffectItem(EffectItemType.ExtraPoints ,shape, image, ev);
    }
}