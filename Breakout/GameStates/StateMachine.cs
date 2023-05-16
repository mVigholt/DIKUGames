namespace Breakout.GameStates;

using System;
using Breakout.Events;
using DIKUArcade.Events;
using DIKUArcade.State;

///<summary>Handle the transition of different states and process inputs</summary>
public class StateMachine : IGameEventProcessor {
    private GameEventBus eventBus = GameBus.GetBus();
    public IGameState ActiveState {
        get; private set;
    }
    public StateMachine() {
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
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
            case (GameLost, GameStateType.MainMenu):
            case (GameWon, GameStateType.MainMenu):
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
        EventDTO ev = new EventDTO(gameEvent);
        SwitchState(ev.StateType.Value);
    }
}