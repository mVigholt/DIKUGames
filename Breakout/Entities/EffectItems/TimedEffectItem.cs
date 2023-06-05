namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;

public class TimedEffectItem : EffectItem {

    public override GameEvent ActivationEvent { get; }
    public virtual GameEvent DeactivationEvent { get; }
    public virtual TimePeriod Duration { get; }


    public TimedEffectItem(
        DynamicShape shape,
        IBaseImage image,
        GameEvent activationEvent,
        GameEvent deactivationEvent,
        TimePeriod duration
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        ActivationEvent = activationEvent;
        DeactivationEvent = deactivationEvent;
        Duration = duration;
    }
}