namespace Galaga.GalagaStates;

using DIKUArcade.Galaga.GalagaStates;
using DIKUArcade.Events;
using DIKUArcade.State;

///<summary>Handle the transition of different states and process inputs</summary>
public class StateMachine : IGameEventProcessor {
    public IGameState ActiveState {
        get; private set;
    }
    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        ActiveState = MainMenu.GetInstance();
    }
    ///<summary>Make the transition from current state to the next state</summary>
    ///<param name = "NextState"></param>
    ///<return>no return</return>
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
            case (GameRunning, GameStateType.GameLost):
                ActiveState = GameLost.GetInstance();
                ActiveState.ResetState();
                break;
            case (GameRunning, GameStateType.GameWon):
                ActiveState = GameWon.GetInstance();
                ActiveState.ResetState();
                break;
            case (GamePaused, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance();
                ActiveState.ResetState();
                break;
            case (GamePaused, GameStateType.GameRunning):
                ActiveState = GameRunning.GetInstance();
                break;
            case (GameLost, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance();
                ActiveState.RenderState();
                break;
            case (GameWon, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance();
                ActiveState.RenderState();
                break;
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        SwitchState(ev.StateType.Value);
    }
}