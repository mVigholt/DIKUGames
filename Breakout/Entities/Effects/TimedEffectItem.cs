namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class TimedEffectItem : EffectItem {

    public override TimedGameEvent ActivationEvent { get; }
    public virtual TimedGameEvent DeactivationEvent { get; }
    public virtual int TimeLeft { get; }

    public TimedEffectItem(
        DynamicShape shape,
        IBaseImage image,
        TimedGameEvent activationEvent,
        TimedGameEvent deactivationEvent,
        int timeLeft
    )
        : base(shape, image, activationEvent) {
        // Consider a builder
        DeactivationEvent = deactivationEvent;
        TimeLeft = timeLeft;
    }
}