namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class MoveableEntity : Entity {

    private protected DynamicShape shape;

    private Vec2F dir = new Vec2F(0,0);

    // private float moveLeft = 0.0f;

    // private float moveRight = 0.0f;

    // private float moveUp = 0.0f;

    // private float moveDown = 0.0f;

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

    public Vec2F GetExtent() {
        return shape.Extent.Copy();
    }

    public void Render() {
        RenderEntity();
    }

    private protected void UpdateDirection() {
        float C = (float) System.Math.Sqrt(System.Math.Pow((dir.X), 2) + System.Math.Pow(dir.Y, 2));
        C = (C != 0 ? C : 1); 
        var D = new Vec2F((SPEED * dir.X / C), (SPEED * dir.Y / C));
        shape.ChangeDirection(D);

        //shape.ChangeDirection(new Vec2F(moveLeft + moveRight, moveUp + moveDown));
    }

    protected void ChangeDir(CollisionDirection colDir, Vec2F ColidingObjectVector) {
        switch (colDir) {
            case (CollisionDirection.CollisionDirLeft):
            case (CollisionDirection.CollisionDirRight):
                dir.X *= -1;
                break;
            case (CollisionDirection.CollisionDirUp):
            case (CollisionDirection.CollisionDirDown):
                dir.Y *= -1;
                break;
            default:
                break;
        }
        dir += ColidingObjectVector;   
        UpdateDirection();
    }

    // private protected void SetMoveLeft(bool val) {
    //     moveLeft = -SPEED * (val ? 1 : 0);
    //     UpdateDirection();
    // }

    // private protected void SetMoveRight(bool val) {
    //     moveRight = SPEED * (val ? 1 : 0);
    //     UpdateDirection();
    // }

    // private protected void SetMoveUp(bool val) {
    //     moveUp = SPEED * (val ? 1 : 0);
    //     UpdateDirection();
    // }

    // private protected void SetMoveDown(bool val) {
    //     moveDown = -SPEED * (val ? 1 : 0);
    //     UpdateDirection();
    // }

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