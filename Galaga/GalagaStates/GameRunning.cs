namespace Galaga.GalagaStates;

using System;
using DIKUArcade.Input;
using DIKUArcade.State;

public class GameRunning : IGameState {
    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
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