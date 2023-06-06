namespace Breakout.Entities;

using Breakout.Events;
using Breakout.GameStates;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;

///<summary>
/// A shuttle (player) class inherites from MoveableEntity and IGameEventProcessor
/// This class is created as a singleton, since it will be created in
/// every time in each level and will be created again.
/// This class will subscribe and hear from a PlayerEvent.
///</summary>
public class Shuttle : MoveableEntity, IGameEventProcessor {

    public static readonly Vec2F STD_EXTENT = new Vec2F(0.15f, 0.03f);
    private static Shuttle instance = null;
    private GameEventBus eventBus;

    private Shuttle(Vec2F position, IBaseImage image)
        : base(new DynamicShape(position, STD_EXTENT), image, 0.02f) {
    }

    ///<summary>To put the shuttle back to its original
    ///position and extent. Expecially when a new level is created</summary>
    public static void ResetShuttle(Vec2F position) {
        Shuttle.instance.Shape.SetPosition(position);
        Shuttle.instance.Shape.Extent = STD_EXTENT;
    }

    public static Shuttle NewShuttle(Vec2F position, IBaseImage image) {
        if (Shuttle.instance == null) {
            Shuttle.instance = new Shuttle(position, image);
            Shuttle.instance.InitEventBus();
        }
        ResetShuttle(position);
        return Shuttle.instance;
    }

    private void InitEventBus() {
        eventBus = GameBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
    }

    private protected void SetMoveLeft(bool val) {
        UpdateDirection(new Vec2F(val ? -1 : 1, 0));
    }

    private protected void SetMoveRight(bool val) {
        UpdateDirection(new Vec2F(val ? 1 : -1, 0));
    }

    ///<summary>Process Shuttle event</summary>
    ///<param name = "gameEvent">The input event</param>
    ///<return>no return</return>
    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        if (ev.Message == "Stop") {
            Stop();
            return;
        }
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