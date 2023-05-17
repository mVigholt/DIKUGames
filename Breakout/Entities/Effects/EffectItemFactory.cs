namespace Breakout.Entities.Effects;


using System;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.IO;
using Breakout.Events;



public class EffectItemFactory {
    // It is not clear what this factory should produce

    private readonly int STD_DURATION = 5000;

    private GameEvent CreateEvent(string message) {
        Console.WriteLine(EffectItemType.ExtraLife.ToString());
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(message)
            .Build();
    }

    public void RandomPowerUp() {

    }

    public void RandomHazard() {}

    public InstantEffectItem ExtraLife(Vec2F position) {
        Vec2F extent = new Vec2F(0.05f, 0.05f);
        DynamicShape shape = new DynamicShape(position, extent);
        Image image = Assets.LoadImage("heart_filled.png");
        GameEvent ev = CreateEvent(EffectItemType.ExtraLife.ToString());
        return new InstantEffectItem(shape, image, ev);
    }
}