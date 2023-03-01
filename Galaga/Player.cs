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
    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        Vec2F pos = GetPosition();
        float maxX = 1.0f - shape.Extent.X;
        float maxY = 1.0f - shape.Extent.Y;
        if (pos.X < 0) {
            pos.X = 0;
        } else if (pos.X >= maxX) {
            pos.X = maxX;
        }
        if (pos.Y < 0) {
            pos.Y = 0;
        }
        else if (pos.Y >= maxY) {
            pos.Y = maxY;
        }
        shape.Move();
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