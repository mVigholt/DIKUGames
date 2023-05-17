namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class TimedEffectItem : EffectItem {

    public override GameEvent ActivationEvent { get; }
    public virtual GameEvent DeactivationEvent { get; }
    public virtual int TimeLeft { get; }

    public TimedEffectItem(
        DynamicShape shape,
        IBaseImage image,
        GameEvent activationEvent,
        GameEvent deactivationEvent,
        int timeLeft
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        DeactivationEvent = deactivationEvent;
        TimeLeft = timeLeft;
    }
}