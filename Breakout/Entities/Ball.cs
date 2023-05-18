namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.IO;

public class Ball : MoveableEntity {
    public static readonly Vec2F STD_EXTEND = new Vec2F(0.03f, 0.03f);

    public int damage { get; private set; } = 1;

    public Ball(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, STD_EXTEND), image, 0.015f) {
    }

    public static Ball At(Vec2F position) {
        IBaseImage image = Assets.LoadImage("ball.png");
        System.Console.WriteLine("Ball at " + position);
        return new Ball(position, image);
    }

    public override void Move() {
        if (this.GetPosition().X == 0.0f) {
            this.UpdateDirection(CollisionDirection.CollisionDirRight, new Vec2F(0, 0));
        }
        if (this.GetPosition().X == 1.0f - this.GetExtent().X) {
            this.UpdateDirection(CollisionDirection.CollisionDirLeft, new Vec2F(0, 0));
        }
        if (this.GetPosition().Y == 1.0f - this.GetExtent().Y) {
            this.UpdateDirection(CollisionDirection.CollisionDirDown, new Vec2F(0, 0));
        }
        if (this.GetPosition().Y == 0.0f) {
            this.DeleteEntity();
        }
        base.Move();
    }
}
