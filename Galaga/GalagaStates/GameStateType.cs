<<<<<<< HEAD
namespace DIKUArcade.Galaga.GalagaStates;
=======
namespace DIKUArcade.Galaga.GalagaState;
>>>>>>> main

using System;

public enum GameStateType {
    MainMenu,
    GameRunning,
    GamePaused
}

<<<<<<< HEAD
public class StateTransformer {
=======
public static class StateTransformer {
>>>>>>> main
    public static GameStateType TransformStringToState(string state) {
        GameStateType enumOut;
        if (Enum.TryParse<GameStateType>(state, true, out enumOut)) {
            return enumOut;
        }
<<<<<<< HEAD
        throw new ArgumentException();
        
    }
=======
        throw new ArgumentException();     
    }

>>>>>>> main
    public static string TransformStateToString(GameStateType state) {
        return state.ToString();           
    }
}