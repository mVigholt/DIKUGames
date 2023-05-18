namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class TimedEffectItem : EffectItem {

    public override EffectItemType Type { get; }
    public override GameEvent ActivationEvent { get; }
    public virtual GameEvent DeactivationEvent { get; }
    public virtual int TimeLeft { get; }


    public TimedEffectItem(
        EffectItemType type,
        DynamicShape shape,
        IBaseImage image,
        GameEvent activationEvent,
        GameEvent deactivationEvent,
        int timeLeft
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        Type = type;
        ActivationEvent = activationEvent;
        DeactivationEvent = deactivationEvent;
        TimeLeft = timeLeft;
    }
}