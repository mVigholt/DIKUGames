namespace Breakout.Entities;

using Breakout.Events;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;

public class Player : MoveableEntity, IGameEventProcessor {
    private GameEventBus eventBus;

    public int Level {
        get;
        internal set;
    }

    public Player(DynamicShape shape, IBaseImage image)
        : base(shape, image, 0.01f) {
        this.Level = 0;
        InitEventBus();
    }

    private void InitEventBus() {
        eventBus = GameBus.GetBus();
        eventBus.Subscribe(GameEventType.PlayerEvent, this);
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