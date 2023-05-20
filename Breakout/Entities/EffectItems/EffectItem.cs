namespace Breakout.Entities.EffectItems;

using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using Breakout.Events;

public abstract class EffectItem : MoveableEntity, ICloneable
{
    public abstract GameEvent ActivationEvent { get; }

    // Todo: Should not receive argument ev
    public EffectItem(DynamicShape shape, IBaseImage image, GameEvent ev)
        : base(shape, image) {
        shape.ChangeDirection(new Vec2F(0f, -0.01f));
    }

    protected GameEvent CreateEvent(string message) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(message)
            .Build();
    }

    public object Clone() {
        EffectItem cloned = (EffectItem)MemberwiseClone();
        return cloned;
    }
}