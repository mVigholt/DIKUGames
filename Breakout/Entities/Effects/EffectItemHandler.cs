namespace Breakout.Entities.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Events;


public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler instance = null;
    private Dictionary<string, Action> _responses;

    private EffectItemHandler() {
        _responses = new Dictionary<string, Action>();
    }

    public void SetResponseTo(string message, Action action) {
        _responses[message] = action;
    }

    public static EffectItemHandler GetInstance() {
        if (EffectItemHandler.instance == null) {
            EffectItemHandler.instance = new EffectItemHandler();
        }
        return EffectItemHandler.instance;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        EventDTO ev = new EventDTO(gameEvent);
        if (!(_responses.ContainsKey(ev.Message))) {
            Console.WriteLine(
                $"EffectItemHandler does not have a response for: {ev.Message}"
            );
            return;
        }
        _responses[ev.Message]();
    }
}