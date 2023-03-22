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
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        GameEventType eventType = gameEvent.EventType;
        IGameState state = ActiveState;
        KeyboardKey key = (KeyboardKey) gameEvent.ObjectArg1;
        KeyboardAction keyboardAction = (KeyboardAction) gameEvent.IntArg1;


    }

}