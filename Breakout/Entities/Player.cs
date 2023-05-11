namespace Breakout.Entities;

using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;

public class Player : MoveableEntity, IGameEventProcessor {
    public static readonly Vec2F STD_EXTEND = new Vec2F(0.15f, 0.03f); 
    private static Player instance = null;
    private GameEventBus eventBus;

    public int Level {
        get;
        internal set;
    }

    private Player(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, STD_EXTEND), image, 0.02f) {
    }

    private void ResetPlayer(Vec2F position) {
        Player.instance.Level = 0;
        Player.instance.Shape.SetPosition(position);
        Player.instance.Shape.Extent = STD_EXTEND;
    }

    public static Player NewPlayer(Vec2F position, IBaseImage image) {
        if (Player.instance == null) {
            Player.instance = new Player(position, image);
            Player.instance.InitEventBus();
        }
        return Player.instance;
    }

    private void InitEventBus() {
        eventBus = GameBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }

    private protected void SetMoveLeft(bool val) {
        UpdateDirection(CollisionDirection.CollisionDirUnchecked, 
            new Vec2F((val ? -1 : 1), 0));
    }

    private protected void SetMoveRight(bool val) {
        UpdateDirection(CollisionDirection.CollisionDirUnchecked, 
            new Vec2F((val ? 1 : -1), 0));
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