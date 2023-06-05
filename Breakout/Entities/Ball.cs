namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.IO;

public class Ball : MoveableEntity {
    public static readonly Vec2F STD_EXTENT = new Vec2F(0.03f, 0.03f);
    public static readonly float STD_SPEED = 0.015f;

    public bool IsHard { get; set; } = false;
    public int Damage { get; private set; } = 1;

    public Ball(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, STD_EXTENT), image, STD_SPEED) {
    }

    /// <summary>Create a Ball at position</summary>
    public static Ball At(Vec2F position) {
        IBaseImage image = Assets.LoadImage("ball.png");
        return new Ball(position, image);
    }

    public override void Move() {
        if (this.GetPosition().X == 0.0f) {
            this.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirRight);
        }
        if (this.GetPosition().X == 1.0f - this.GetExtent().X) {
            this.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirLeft);
        }
        if (this.GetPosition().Y == 1.0f - this.GetExtent().Y) {
            this.UpdateDirection(new Vec2F(0, 0), CollisionDirection.CollisionDirDown);
        }
        if (this.GetPosition().Y == 0.0f) {
            this.DeleteEntity();
        }
        base.Move();
    }
}
