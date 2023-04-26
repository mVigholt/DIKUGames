namespace Breakout.GameStates;

using System;

public enum GameStateType {
    MainMenu,
    GameRunning,
    GamePaused,
    GameWon,
    GameLost
}

/// <summary> Transfor the GameStateType by string and the other way around </summary>
public class StateTransformer {
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