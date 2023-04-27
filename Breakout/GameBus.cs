namespace Breakout;

using System.Collections.Generic;
using DIKUArcade.Events;

public static class GameBus {
    private static GameEventBus eventBus;
    public static GameEventBus GetBus() {
        return eventBus ?? NewBus();
    }

    ///<summary>Initialize the eventbus by putting the used event
    ///in to the list.</summary>
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