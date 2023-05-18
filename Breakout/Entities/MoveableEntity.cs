namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class MoveableEntity : Entity {

    private protected DynamicShape shape;

    private Vec2F dir = new Vec2F(0, 0);

    public readonly float SPEED;

    public MoveableEntity(DynamicShape shape, IBaseImage image) : this(shape, image, 0.0f) {
    }

    public MoveableEntity(DynamicShape shape, IBaseImage image, float SPEED)
        : base(shape, image) {
        this.shape = shape;
        this.SPEED = SPEED;
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
        shape.ChangeDirection(new Vec2F(SPEED * dir.X, SPEED * dir.Y));
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