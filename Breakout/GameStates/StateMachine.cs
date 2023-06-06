namespace Breakout.GameStates;

using Breakout.Events;
using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Timers;

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
    ///<param name = "NextState">The next state which is going to show</param>
    ///<return>no return</return>
    private void SwitchState(GameStateType NextState, string message) {
        switch (ActiveState, NextState) {
            case (MainMenu, GameStateType.GameRunning):
                ActiveState = GameRunning.GetInstance(true);
                break;
            case (GameRunning, GameStateType.GamePaused):
                StaticTimer.PauseTimer();
                ActiveState = GamePaused.GetInstance(true);
                break;
            case (GameRunning, GameStateType.GameLost):
                ActiveState = GameLost.GetInstance(true);
                break;
            case (GameRunning, GameStateType.GameWon):
                ActiveState = GameWon.GetInstance(true);
                break;
            case (GamePaused, GameStateType.MainMenu):
            case (GameLost, GameStateType.MainMenu):
            case (GameWon, GameStateType.MainMenu):
                ActiveState = MainMenu.GetInstance(true);
                break;
            case (GamePaused, GameStateType.GameRunning):
                StaticTimer.ResumeTimer();
                ActiveState = GameRunning.GetInstance();
                if (message == "Next Level") {
                    GameRunning.GetInstance().ChangeLevel();
                }
                break;
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        string message = ev.Message;
        SwitchState(ev.StateType.Value, message);
    }
}