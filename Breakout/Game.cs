namespace Breakout;

using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.GameStates;
using Breakout.Events;

public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus;
    private StateMachine stateMachine;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitEventBus();
        stateMachine = new StateMachine();
    }

    ///<summary> create new GameEventBus instance and
    ///subscribe it to a proper GameEventType</summary>
    public void InitEventBus() {
        eventBus = GameBus.GetBus();
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
    }

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }

    ///<summary>call different methods in each game loop</summary>
    public override void Update() {
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }

    ///<summary>Process incoming game event.
    ///Her it handles only WindowEvent</summary>
    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        switch (ev.Type, ev.Action.Value) {
            case (GameEventType.WindowEvent, KeyboardAction.KeyPress):
                window.CloseWindow();
                break;
            default:
                break;
        }
    }

    ///<summary>Register keyboardAction to key press or key release</summary>
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }
}