namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

public abstract class EffectItem : MoveableEntity
{
    public abstract TimedGameEvent ActivationEvent { get; }

    public EffectItem(DynamicShape shape, IBaseImage image, TimedGameEvent ev)
        : base(shape, image) {}

}