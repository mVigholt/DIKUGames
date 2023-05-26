namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class MoveableEntity : Entity {

    private protected DynamicShape shape;

    private Vec2F dir = new Vec2F(0, 0);

    public float Speed { get; set; }

    public MoveableEntity(DynamicShape shape, IBaseImage image) : this(shape, image, 0.0f) {
    }

    public MoveableEntity(DynamicShape shape, IBaseImage image, float speed)
        : base(shape, image) {
        this.shape = shape;
        this.Speed = speed;
    }

    public Vec2F GetPosition() {
        return shape.Position.Copy();
    }

    public Vec2F GetDirection() {
        return shape.Direction.Copy();
    }

    public Vec2F GetExtent() {
        return shape.Extent.Copy();
    }

    public void Render() {
        RenderEntity();
    }

    private Vec2F UnitVector(Vec2F vector) {
        float hyp = (float) System.Math.Sqrt(System.Math.Pow(vector.X, 2) + System.Math.Pow(vector.Y, 2));
        hyp = hyp != 0 ? hyp : 1;
        return new Vec2F(vector.X / hyp, vector.Y / hyp);
    }

    public void UpdateDirection(CollisionDirection colDir, Vec2F addVector) {
        switch (colDir) {
            case CollisionDirection.CollisionDirLeft:
            case CollisionDirection.CollisionDirRight:
                dir.X *= -1;
                break;
            case CollisionDirection.CollisionDirUp:
            case CollisionDirection.CollisionDirDown:
                dir.Y *= -1;
                break;
            default:
                break;
        }
        dir = UnitVector(UnitVector(dir) + UnitVector(addVector));
        //entities cannot bounce back and forth and an endless loop
        if (colDir != CollisionDirection.CollisionDirUnchecked &&
            (dir.X == 0 || dir.Y == 0)) {
            var rand = new System.Random().Next(-1, 2);
            if (dir.X == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(rand, 0));
            } else if (dir.Y == 0) {
                dir = UnitVector(UnitVector(dir) + new Vec2F(rand, 0)); }   
        }
        shape.ChangeDirection(new Vec2F(Speed * dir.X, Speed * dir.Y));
    }

    public void Stop() {
        this.UpdateDirection(
            CollisionDirection.CollisionDirUnchecked,
            -1 * this.GetDirection()
        );
    }

    protected Vec2F MinCorner() {
        return new Vec2F(0.0f, 0.0f);
    }

    protected Vec2F MaxCorner() {
        return new Vec2F(1.0f - shape.Extent.X, 1.0f - shape.Extent.Y);
    }

    virtual public void Move() {
        shape.Move();
        if (shape.Position.X < MinCorner().X) {
            shape.Position.X = MinCorner().X;
        }
        if (shape.Position.X > MaxCorner().X) {
            shape.Position.X = MaxCorner().X;
        }
        if (shape.Position.Y < MinCorner().Y) {
            shape.Position.Y = MinCorner().Y;
        }
        if (shape.Position.Y > MaxCorner().Y) {
            shape.Position.Y = MaxCorner().Y;
        }
    }
}