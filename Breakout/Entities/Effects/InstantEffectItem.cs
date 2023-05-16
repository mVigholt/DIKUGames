namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public class InstantEffectItem : EffectItem
{
    public override TimedGameEvent ActivationEvent { get; }

    public InstantEffectItem(DynamicShape shape, IBaseImage image, TimedGameEvent activationEvent)
        : base(shape, image, activationEvent)
    {
        ActivationEvent = activationEvent;
    }
}