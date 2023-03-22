namespace Galaga;

using DIKUArcade.Events;
using System.Collections.Generic;

public static class GalagaBus {
    private static GameEventBus eventBus;
    public static GameEventBus GetBus() {
        return eventBus ?? NewBus();
    }

    private static GameEventBus NewBus() {
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(
            new List<GameEventType> {
                GameEventType.InputEvent,
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent
            }
        );
        return eventBus;
    }
}