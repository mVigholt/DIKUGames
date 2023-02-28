using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;


public class Player
{
    private Entity entity;

    private DynamicShape shape;

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        if (CanMove()) {
            shape.Move();
        }
    }

    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public void SetMoveLeft(bool val) {
        moveLeft = -MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    public void SetMoveRight(bool val) {
        moveRight = MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private bool CanMoveLeft() {
        // Todo: When we implement AABB collision,
        //       use it in this method.
        return shape.Position.X > 0.0f;
    }

    private bool CanMoveRight() {
        return shape.Position.X < (1.0f - shape.Extent.X);
    }

    private MoveDir CurrentDirection() {
        if (moveLeft < 0) {
            return MoveDir.LEFT;
        }
        if (moveRight > 0) {
            return MoveDir.RIGHT;
        }
        return MoveDir.NONE;
    }
    
    private bool CanMove() {
        switch (CurrentDirection()) {
            case MoveDir.LEFT:
                return CanMoveLeft();
            case MoveDir.RIGHT:
                return CanMoveRight();
            default:
                return false;
        }
    }


}


enum MoveDir {
    LEFT,
    RIGHT,
    NONE,
}