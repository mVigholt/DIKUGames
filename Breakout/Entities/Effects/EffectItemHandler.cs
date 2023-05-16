namespace Breakout.Entities.Effects;


using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout.IO;
using Breakout.Events;



public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler instance = null;

    private EffectItemHandler() {
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
    }

    public static EffectItemHandler GetInstance() {
        if (EffectItemHandler.instance == null) {
            EffectItemHandler.instance = new EffectItemHandler();
        }
        return EffectItemHandler.instance;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        System.Console.WriteLine(
            $"EffectItemHandler received an event: {ev.Message}"
        );
        throw new System.NotImplementedException();
    }
}