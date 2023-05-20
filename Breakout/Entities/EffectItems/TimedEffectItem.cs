namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;

public class TimedEffectItem : EffectItem {

    public override GameEvent ActivationEvent { get; }
    public virtual GameEvent DeactivationEvent { get; }
    public virtual TimePeriod TimeLeft { get; }


    public TimedEffectItem(
        DynamicShape shape,
        IBaseImage image,
        GameEvent activationEvent,
        GameEvent deactivationEvent,
        TimePeriod timeLeft
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        ActivationEvent = activationEvent;
        DeactivationEvent = deactivationEvent;
        TimeLeft = timeLeft;
    }
}