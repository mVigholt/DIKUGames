namespace Galaga.GalagaStates;

//using DIKUArcade.EventBus;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.GUI;

public class StateMachine : IGameEventProcessor {
public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        // GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
        GameRunning.GetInstance();
        GamePaused.GetInstance();
    }

    private void SwitchState(GameStateType NextState) {
        switch (ActiveState, NextState) {
            case (MainMenu, GameStateType.GameRunning):
                ActiveState = GameRunning.GetInstance();
                ActiveState.ResetState();
                break;
            case (GameRunning, GameStateType.GamePaused):
                ActiveState = GamePaused.GetInstance();
                ActiveState.ResetState();
                break;
            case (GameRunning, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance();
                ActiveState.ResetState();
                break;
            case (GamePaused, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance();
                ActiveState.ResetState();
                break;
            case (GamePaused, GameStateType.GameRunning):
                ActiveState = GameRunning.GetInstance();
                break;
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        GameEventType? eventType = gameEvent.EventType;
        System.Enum key = (System.Enum)gameEvent.ObjectArg1;
        KeyboardAction? action = (KeyboardAction?)gameEvent.IntArg1;

        switch (eventType, key, action) {
            case (GameEventType.GameStateEvent, GameStateType.MainMenu, _):
                SwitchState((GameStateType)key);
                break;
            case (GameEventType.GameStateEvent, GameStateType.GameRunning, _):
                SwitchState((GameStateType)key);
                break;
            case (GameEventType.GameStateEvent, GameStateType.GamePaused, _):
                SwitchState((GameStateType)key);
                break;
            default:
                break;
        }
    }
}