namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

public abstract class EffectItem : MoveableEntity
{
    public abstract GameEvent ActivationEvent { get; }

    public EffectItem(DynamicShape shape, IBaseImage image, GameEvent ev)
        : base(shape, image) {
        shape.ChangeDirection(new Vec2F(0f, -0.01f));
    }

    public override void Move() {
        if (GetPosition().Y == 0.0f) {
            DeleteEntity();
        }
        base.Move();
    }
}