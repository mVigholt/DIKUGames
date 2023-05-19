namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;

public class TimedEffectItem : EffectItem {

    public override EffectItemType Type { get; }
    public override GameEvent ActivationEvent { get; }
    public virtual GameEvent DeactivationEvent { get; }
    public virtual TimePeriod TimeLeft { get; }


    public TimedEffectItem(
        EffectItemType type,
        DynamicShape shape,
        IBaseImage image,
        GameEvent activationEvent,
        GameEvent deactivationEvent,
        TimePeriod timeLeft
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        Type = type;
        ActivationEvent = activationEvent;
        DeactivationEvent = deactivationEvent;
        TimeLeft = timeLeft;
    }
}