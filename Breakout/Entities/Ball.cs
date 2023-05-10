namespace Breakout.Entities;

using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class Ball : MoveableEntity{
    private GameEventBus eventBus;

    public Ball(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, new Vec2F(0.03f, 0.03f)), image, 0.01f) {
    }

    public override void Move() {
        if (this.GetPosition().X == 0.0f) {
            this.UpdateDirection(CollisionDirection.CollisionDirRight, new Vec2F(0,0));
        }
        if (this.GetPosition().X == 1.0f - this.GetExtent().X) {
            this.UpdateDirection(CollisionDirection.CollisionDirLeft, new Vec2F(0,0));
        }
        if (this.GetPosition().Y == 1.0f - this.GetExtent().Y){
            this.UpdateDirection(CollisionDirection.CollisionDirDown, new Vec2F(0,0));
        }
        if (this.GetPosition().Y == 0.0f){
            this.DeleteEntity();
        }
        base.Move();
    }
}
