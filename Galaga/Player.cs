namespace Galaga;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


public class Player {
    private Entity entity;

    private DynamicShape shape;

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private float moveUp = 0.0f;

    private float moveDown = 0.0f;

    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }
    public Vec2F GetPosition() {
        return shape.Position;
    }

    public Shape GetShape(){
        return shape;
    }
    public void Render() {
        entity.RenderEntity();
    }

    private Vec2F minCorner() {return new Vec2F(0.0f, 0.0f);}

    private Vec2F maxCorner() {return new Vec2F(1.0f - shape.Extent.X, 1.0f - shape.Extent.Y);}

    public void Move() {
        shape.Move();

        if (shape.Position.X < minCorner().X) {
            shape.Position.X = minCorner().X;
        }
        if (shape.Position.X > maxCorner().X) {
            shape.Position.X = maxCorner().X;
        }
        if (shape.Position.Y < minCorner().Y) {
            shape.Position.Y = minCorner().Y;
        }
        if (shape.Position.Y > maxCorner().Y) {
            shape.Position.Y = maxCorner().Y;
        }
    }

    private void UpdateDirection() {
        shape.ChangeDirection(new Vec2F(moveLeft + moveRight, moveUp + moveDown));
    }

    public void SetMoveLeft(bool val) {
        moveLeft = -MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    public void SetMoveRight(bool val) {
        moveRight = MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    public void SetMoveUp(bool val) {
        moveUp = MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    public void SetMoveDown(bool val) {
        moveDown = -MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }
}