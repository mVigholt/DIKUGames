namespace Breakout.Entities.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Events;


public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler instance = null;
    private Dictionary<EffectItemType, IEffect> _effects;

    private EffectItemHandler() {
        _effects = new Dictionary<EffectItemType, IEffect>();
    }

    public void AddEventHandler(IEffect effect) {
        _effects[effect.Type] = effect;
    }

    public static EffectItemHandler GetInstance() {
        if (EffectItemHandler.instance == null) {
            EffectItemHandler.instance = new EffectItemHandler();
        }
        return EffectItemHandler.instance;
    }

    public void ProcessEvent(GameEvent gameEvent) {
        string msg = gameEvent.Message;
        EffectItemType type;
        Enum.TryParse<EffectItemType>(gameEvent.Message, out type);
        Console.WriteLine("Type is " + type.ToString());
    }
}