namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class MoveableEntity : Entity {

    private protected DynamicShape shape;

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private float moveUp = 0.0f;

    private float moveDown = 0.0f;

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

    private void UpdateDirection() {
        shape.ChangeDirection(new Vec2F(moveLeft + moveRight, moveUp + moveDown));
    }

    private protected void SetMoveLeft(bool val) {
        moveLeft = -SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private protected void SetMoveRight(bool val) {
        moveRight = SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private protected void SetMoveUp(bool val) {
        moveUp = SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private protected void SetMoveDown(bool val) {
        moveDown = -SPEED * (val ? 1 : 0);
        UpdateDirection();
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