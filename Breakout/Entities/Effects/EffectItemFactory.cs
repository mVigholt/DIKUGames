namespace Breakout.Entities.Effects;


using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.IO;
using Breakout.Events;



public class EffectItemFactory {

    private readonly int STD_DURATION = 5000;

    private TimedGameEvent CreateTimedEvent(string message) {
        GameEvent ev = new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(message)
            .Build();
        return new TimedGameEvent(
            TimePeriod.NewMilliseconds(STD_DURATION), ev
        );
    }

    public InstantEffectItem ExtraLife() {
        // pos and extent: arbitrary values
        Vec2F pos = new Vec2F(0f, 0f);
        Vec2F extent = new Vec2F(0.05f, 0.05f);
        DynamicShape shape = new DynamicShape(pos, extent);
        Image image = Assets.LoadImage("heart_filled.png");
        TimedGameEvent ev = CreateTimedEvent("EXTRA_LIFE");
        return new InstantEffectItem(shape, image, ev);
    }
}