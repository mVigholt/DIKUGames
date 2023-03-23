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

    private void SwitchState(GameStateType stateType) {
        switch (stateType) {
            case GameStateType.MainMenu:
                ActiveState.ResetState();
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GameRunning.GetInstance();
                break;
            default:
                break;
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        GameEventType eventType = gameEvent.EventType;
        string message = gameEvent.Message;
        string gameState = gameEvent.StringArg1;
        // System.Console.WriteLine("String is:" + gameState);
        GameStateType stateType = StateTransformer.TransformStringToState(gameState);
        KeyboardKey key = (KeyboardKey) gameEvent.ObjectArg1;
        KeyboardAction keyboardAction = (KeyboardAction) gameEvent.IntArg1;
        // System.Console.WriteLine("Active is:" + ActiveState);
        switch(stateType, message){
            case (_, "CHANGE_STATE"):
                SwitchState(stateType);
                break;
        }
        ActiveState.HandleKeyEvent(keyboardAction, key);
    }

}