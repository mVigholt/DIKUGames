namespace DIKUArcade.Galaga.GalagaStates
{
    public enum GameStateType {
        MainMenu,
        GameRunning,
        GamePaused
    }

    public class StateTransformer {
        public static GameStateType TransformStringToState(string state) {
            return GameStateType.MainMenu;
        }
        public static string TransformStateToString(GameStateType state) {
            return "MainMenu";
        }
    }
}