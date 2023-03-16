namespace Galaga;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;

public class Player : Entity, IGameEventProcessor {
    private DynamicShape shape;

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private float moveUp = 0.0f;

    private float moveDown = 0.0f;

    private const float MOVEMENT_SPEED = 0.01f;
    public Health health;

    public Player(DynamicShape shape, IBaseImage image) 
        : base(shape, image) {
        this.shape = shape;
        int startingHealth = 3;
        health = new Health(shape.Position, shape.Extent, startingHealth);
    }

    public void LoseHealth() {
        health.LoseHealth();
    }

    public bool IsDead() {
        return health.Points == 0;
    }

    public Vec2F GetPosition() {
        return shape.Position.Copy();
    }

    public Vec2F GetExtent(){
        return shape.Extent.Copy();
    }

    public void Render() {
        RenderEntity();
    }

    private Vec2F MinCorner() {return new Vec2F(0.0f, 0.0f);}

    private Vec2F MaxCorner() {return new Vec2F(1.0f - shape.Extent.X, 1.0f - shape.Extent.Y);}

    public void Move() {
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

    private void UpdateDirection() {
        shape.ChangeDirection(new Vec2F(moveLeft + moveRight, moveUp + moveDown));
    }

    private void SetMoveLeft(bool val) {
        moveLeft = -MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        moveRight = MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private void SetMoveUp(bool val) {
        moveUp = MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    private void SetMoveDown(bool val) {
        moveDown = -MOVEMENT_SPEED * (val ? 1 : 0);
        UpdateDirection();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        GameEventType? eventType = gameEvent.EventType;
        KeyboardKey? key = (KeyboardKey?)gameEvent.ObjectArg1;
        KeyboardAction? action = (KeyboardAction?)gameEvent.IntArg1;
        
        switch (eventType, key, action) {
            case (_, KeyboardKey.Left, _):
                SetMoveLeft(action == KeyboardAction.KeyPress);
                break; 
            case (_, KeyboardKey.Right, _):
                SetMoveRight(action == KeyboardAction.KeyPress);
                break;
            case (_, KeyboardKey.Up, _):
                SetMoveUp(action == KeyboardAction.KeyPress);
                break;
            case (_, KeyboardKey.Down, _):
                SetMoveDown(action == KeyboardAction.KeyPress);
                break;
            default:
                break;
        }
    }
}