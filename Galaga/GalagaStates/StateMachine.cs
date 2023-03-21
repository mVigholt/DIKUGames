namespace Galaga.GalagaStates;

using DIKUArcade.State;
using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Events;
using DIKUArcade.Events.Generic;

//using DIKUArcade.EventBus;

public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState { get; private set; }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        //ActiveState = MainMenu.GetInstance();
    }

    private void SwitchState(GameStateType stateType) {
        switch (stateType) { 
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        throw new System.NotImplementedException();
    }
}