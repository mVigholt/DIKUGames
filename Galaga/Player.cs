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
        if (shape.Direction.X > 0.0f){
            shape.Move();
        }  
    // TODO: move the shape and guard against the window borders
    }

    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;//new DIKUArcade.Math.Vec2F(moveLeft + moveRight, 0.0f);
        //TODO: ?
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft += MOVEMENT_SPEED;
            moveRight -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
            moveRight = 0.0f;
        }
        UpdateDirection();
        // TODO:set moveLeft appropriately and call UpdateDirection()
    }

    public void SetMoveRight(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
            moveRight += MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
            moveRight = 0.0f;
        }
        UpdateDirection();
    // TODO:set moveRight appropriately and call UpdateDirection()
    }
    
}