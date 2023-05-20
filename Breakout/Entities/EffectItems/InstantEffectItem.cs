namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

public class InstantEffectItem : EffectItem
{
    public override GameEvent ActivationEvent { get; }


    public InstantEffectItem(DynamicShape shape, IBaseImage image, GameEvent activationEvent)
        : base(shape, image, activationEvent)
    {
        ActivationEvent = activationEvent;
    }
}