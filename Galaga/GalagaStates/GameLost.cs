namespace Galaga.GalagaStates;

using DIKUArcade.Input;
using DIKUArcade.State;

public class GameLost : IGameState {
    private static GameLost instance = null;
     public static GameLost GetInstance() {
        if (GameLost.instance == null) {
            GameLost.instance = new GameLost();
            GameLost.instance.InitializeGameState();
        }
        return GameLost.instance;
    }
    public void InitializeGameState(){

    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        throw new System.NotImplementedException();
    }

    public void RenderState() {
        throw new System.NotImplementedException();
    }

    public void ResetState() {
        throw new System.NotImplementedException();
    }

    public void UpdateState() {
        throw new System.NotImplementedException();
    }
}