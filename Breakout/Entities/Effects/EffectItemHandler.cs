namespace Breakout.Entities.Effects;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using Breakout.Events;

// Todo: Make non-singleton
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
        EffectItemType type = GetEffectItemType(gameEvent.Message);
        IEffect effect = _effects[type];
        if (gameEvent.Message.Contains("Deactivate")) {
            ((ITimedEffect) effect).Deactivate();
        }
        else {
            effect.Activate();
        }
    }

    private EffectItemType GetEffectItemType(string message) {
        if (message.Contains("Deactivate")) {
            return GetEffectItemType(message.Split("Deactivate")[0]);
        }
        EffectItemType type;
        Enum.TryParse<EffectItemType>(message, out type);
        return type;
    }
}