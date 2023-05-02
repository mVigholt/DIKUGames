namespace Breakout.BreakoutEntities;

using System;
using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;

public class Player : Entity, IGameEventProcessor {
    private GameEventBus eventBus;

    private DynamicShape shape;

    private float moveLeft = 0.0f;

    private float moveRight = 0.0f;

    private float moveUp = 0.0f;

    private float moveDown = 0.0f;

    public const float MOVEMENT_SPEED = 0.01f;

    public int Level {
        get;
        internal set;
    }

    public Player(DynamicShape shape, IBaseImage image) : this(shape, image, 50) {
    }

    public Player(DynamicShape shape, IBaseImage image, int startingHealth)
        : base(shape, image) {
        this.Level = 0;
        this.shape = shape;
        Vec2F pos = new Vec2F(0.0f, -0.2f);
        Vec2F extent = new Vec2F(0.3f, 0.3f);
        InitEventBus();
    }

    private void InitEventBus() {
        eventBus = GameBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
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

    private Vec2F MinCorner() {
        return new Vec2F(0.0f, 0.0f);
    }

    private Vec2F MaxCorner() {
        return new Vec2F(1.0f - shape.Extent.X, 1.0f - shape.Extent.Y);
    }

    public void Move() {
        shape.Move();

        if (shape.Position.X < MinCorner().X) {
            shape.Position.X = MinCorner().X;
        }
        if (shape.Position.X > MaxCorner().X) {
            shape.Position.X = MaxCorner().X;
        }
        // if (shape.Position.Y < MinCorner().Y) {
        //     shape.Position.Y = MinCorner().Y;
        // }
        // if (shape.Position.Y > MaxCorner().Y) {
        //     shape.Position.Y = MaxCorner().Y;
        // }
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

    ///<summary>Process Player event</summary>
    ///<param name = "gameEvent">The input event</param>
    ///<return>no return</return>
    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        switch (ev.Key.Value) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                SetMoveLeft(ev.Action == KeyboardAction.KeyPress);
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                SetMoveRight(ev.Action == KeyboardAction.KeyPress);
                break;
            default:
                break;
        }
    }
}