namespace Breakout.Entities.EffectItems;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

/// <summary>
/// The entity associated with an EffectItem (power-up or hazard).
/// A part from its parent properties and methods, this has
/// an activation event that can be used to trigger an Effect.
/// </summary>
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