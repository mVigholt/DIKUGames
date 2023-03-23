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
        stateMachine = new StateMachine();
    }

    ///<summary> create new GameEventBus instance and
    ///subscribe it to a proper GameEventType</summary>
    public void InitEventBus() {
        eventBus = GalagaBus.GetBus();
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
        stateMachine.ActiveState.HandleKeyEvent(action, key);
    }

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
            case KeyboardKey.M:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = DIKUArcade.Galaga.GalagaStates.GameStateType.MainMenu,
                    }
                );
                break;
            case KeyboardKey.G:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = DIKUArcade.Galaga.GalagaStates.GameStateType.GameRunning,
                    }
                );
                break;
            case KeyboardKey.P:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = DIKUArcade.Galaga.GalagaStates.GameStateType.GamePaused,
                    }
                );
                break;
            case KeyboardKey.Left:
            case KeyboardKey.Right:
            case KeyboardKey.Up:
            case KeyboardKey.Down:
            //     if (stateMachine.ActiveState is MainMenu){
            //         eventBus.RegisterEvent(
            //             new GameEvent{
            //                 EventType = GameEventType.GameStateEvent,
            //                 ObjectArg1 = key,
            //                 StringArg1 = "MainMenu",
            //                 IntArg1 = (int)KeyboardAction.KeyPress
            //             }
            //         );
            //     }
            //     if (stateMachine.ActiveState is GameRunning){
            //         eventBus.RegisterEvent(
            //             new GameEvent {
            //                 EventType = GameEventType.PlayerEvent,
            //                 ObjectArg1 = key,
            //                 StringArg1 = "GameRunning",
            //                 IntArg1 = (int)KeyboardAction.KeyPress
            //             }
            //         );
            //     }
            //     if (stateMachine.ActiveState is GamePaused){
            //         eventBus.RegisterEvent(
            //             new GameEvent {
            //                 EventType = GameEventType.GameStateEvent,
            //                 ObjectArg1 = key,
            //                 StringArg1 = "GamePaused",
            //                 IntArg1 = (int)KeyboardAction.KeyPress
            //             }
            //         );

            //     }
            //     break;
            // case KeyboardKey.Enter:
            //     if (stateMachine.ActiveState is MainMenu){
            //         eventBus.RegisterEvent(
            //         new GameEvent {
            //             EventType = GameEventType.GameStateEvent,
            //             Message = "CHANGE_STATE",
            //             StringArg1 = "MainMenu",
            //             ObjectArg1 = key,
            //             IntArg1 = (int)KeyboardAction.KeyPress
            //         });
            //     }
            //     if (stateMachine.ActiveState is GamePaused){
            //         eventBus.RegisterEvent(
            //         new GameEvent {
            //             EventType = GameEventType.GameStateEvent,
            //             Message = "CHANGE_STATE",
            //             StringArg1 = "GamePaused",
            //             ObjectArg1 = key,
            //             IntArg1 = (int)KeyboardAction.KeyPress
            //         });
            //     }

                break;
            case KeyboardKey.A: //Autoshoot
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        StringArg1 = "GameRunning",
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
                        StringArg1 = "GameRunning",
                        IntArg1 = (int)KeyboardAction.KeyRelease
                    }
                );
                break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent(
                    new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        ObjectArg1 = key,
                        StringArg1 = "GameRunning",
                        IntArg1 = (int)KeyboardAction.KeyRelease,
                    }
                );
                break;
            default:
                break;
        }
    }
}