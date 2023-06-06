namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Breakout.IO;


///<summary>
///A moveable concept for ball. This class inherits MoveableEntity class
///</summary>
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

    public override void UpdateDirection(Vec2F addVector, CollisionDirection colDir) {
        base.UpdateDirection(addVector, colDir);
        //entities cannot bounce back and forth in an endless loop
        if (colDir != CollisionDirection.CollisionDirUnchecked &&
            (dir.X == 0 || dir.Y == 0)) {
            var rand = new System.Random().Next(-1, 2);
            if (dir.X == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(rand, 0));
            } else if (dir.Y == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(0, rand));
            }
        }
        shape.ChangeDirection(new Vec2F(Speed * dir.X, Speed * dir.Y));
    }

    /// <summary>Ensure the ball is within the wall boundary
    /// When it hits the boundary, the wall is not moveable, so
    /// the ball will not be added an extra direction, while
    /// only turn it direction back. See UpdateDirection() method
    ///</summary>
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
