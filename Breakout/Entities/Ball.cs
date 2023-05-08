namespace Breakout.Entities;

using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;

public class Ball : MoveableEntity, IGameEventProcessor {
    private GameEventBus eventBus;
    private Vec2F speed;
    public Vec2F Speed {
        get{
            return this.speed;
        }
        set{
            this.speed = value;
        }
    }

     public int Level {
        get;
        internal set;
    }

    public Ball(DynamicShape shape, IBaseImage image)
        : base(shape, image, 0.01f) {
        this.Level = 0;
        this.speed = new Vec2F(0.0f, 0.0f);
        InitEventBus();
    }


    private void InitEventBus() {
        eventBus = GameBus.GetBus();
        eventBus.Subscribe(GameEventType.InputEvent, this);
    }

    override public void Move(){
        Vec2F curPosition =  this.GetPosition();
        Vec2F curPos = this.Shape.Position;
        Vec2F pos = new Vec2F();
        if (this.GetPosition().X <= MinCorner().X){
            this.Speed.X = -this.Speed.X;
            this.Speed.Y = -this.Speed.Y;
        }
        if (this.GetPosition().Y >= MaxCorner().Y){
            this.Speed.X = -this.Speed.X;
            this.Speed.Y = -this.Speed.Y;
        }
            pos.Y = curPos.Y  + this.Speed.Y;
            pos.X = curPos.X + this.Speed.X;
            this.Shape.SetPosition(pos);
    }


    ///<summary>Process Player event</summary>
    ///<param name = "gameEvent">The input event</param>
    ///<return>no return</return>
    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        switch (ev.Key.Value) {
            case KeyboardKey.Left:
            case KeyboardKey.A:
                if (Speed.X == 0.0f && Speed.Y == 0.0f){
                    Speed.X =  -0.01f;
                    Speed.Y = 0.01f;
                }
                break;
            case KeyboardKey.Right:
            case KeyboardKey.D:
                if (Speed.X == 0.0f && Speed.Y == 0.0f){
                    Speed.X =  0.01f;
                    Speed.Y = 0.01f;
                }
                break;
            default:
                break;
        }
    }
}