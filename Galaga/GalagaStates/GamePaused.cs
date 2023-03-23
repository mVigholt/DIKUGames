namespace Galaga.GalagaStates;

using System;
using DIKUArcade.Input;
using DIKUArcade.State;

public class GamePaused : IGameState {
    private static GamePaused instance = null;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.InitializeGameState();
        }
        return GamePaused.instance;
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

