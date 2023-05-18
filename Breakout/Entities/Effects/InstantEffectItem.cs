namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

public class InstantEffectItem : EffectItem
{
    public override EffectItemType Type { get; }
    public override GameEvent ActivationEvent { get; }


    public InstantEffectItem(EffectItemType type, DynamicShape shape, IBaseImage image, GameEvent activationEvent)
        : base(shape, image, activationEvent)
    {
        Type = type;
        ActivationEvent = activationEvent;
    }
}