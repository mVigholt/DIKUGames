namespace Galaga;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Galaga.GalagaStates;

public class Game : DIKUGame, IGameEventProcessor {
    private GameEventBus eventBus = GalagaBus.GetBus();
    private StateMachine stateMachine;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        InitEventBus();
        ResetState();
    }

    private void ResetState() {
        stateMachine = new StateMachine();
    }

    private void GameOver() {
        ResetState();
    }
    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }

    ///<summary>call different methods in each game loop</summary>
    public override void Update() {
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();

    }

    ///<summary>Register each keypress to a corresponding game event</summary>
    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    }
                );
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                if (stateMachine.ActiveState is MainMenu){
                    eventBus.RegisterEvent(
                        new GameEvent{
                            EventType = GameEventType.GameStateEvent,
                            ObjectArg1 = key,
                            IntArg1 = (int)KeyboardAction.KeyPress
                        }
                    );
                }
                if (stateMachine.ActiveState is GameRunning){
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    }
                );}
                break;
            case KeyboardKey.Enter:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    });
                break;
            case KeyboardKey.A: //Autoshoot
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyPress
                    });
                break;
            default:
                break;
        }
    }

    ///<summary>Register each key release to a corresponding game event</summary>
    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyRelease
                    }
                );
                break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = key,
                        IntArg1 = (int)KeyboardAction.KeyRelease,
                    }
                );
                break;
            default:
                break;
        }
    }

    ///<summary>Register keyboardAction to key press or key release</summary>
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                this.KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                this.KeyRelease(key);
                break;
        }
    }

    ///<summary>The method which is called in the ProcessEvents()
    ///in GameEventBus, to handle each gameEvent</summary>
    public void ProcessEvent(GameEvent gameEvent) {
        GameEventType? eventType = gameEvent.EventType;
        KeyboardKey? key = (KeyboardKey?)gameEvent.ObjectArg1;
        KeyboardAction? action = (KeyboardAction?)gameEvent.IntArg1;

        switch (eventType, key, action) {
            case (GameEventType.WindowEvent, KeyboardKey.Escape, KeyboardAction.KeyPress):
                window.CloseWindow();
                break;
            default:
                break;
        }
    }

    ///<summary> create new GameEventBus instance and
    ///subscribe it to a proper GameEventType</summary>
    public void InitEventBus() {
        eventBus = GalagaBus.GetBus();
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
    }
}