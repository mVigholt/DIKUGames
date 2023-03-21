namespace DIKUArcade.Galaga.GalagaState;

using System;

public enum GameStateType {
    MainMenu,
    GameRunning,
    GamePaused
}

public static class StateTransformer {
    public static GameStateType TransformStringToState(string state) {
        GameStateType enumOut;
        if (Enum.TryParse<GameStateType>(state, true, out enumOut)) {
            return enumOut;
        }
        throw new ArgumentException();     
    }

    public static string TransformStateToString(GameStateType state) {
        return state.ToString();           
    }
}