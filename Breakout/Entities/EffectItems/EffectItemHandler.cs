namespace Breakout.Entities.EffectItems;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using Breakout.Entities.Board;

/// <summary>
/// Listens for events of type GameEventType.StatusEvent.
/// Those are used for power-ups and hazards, AKA effect items.
/// To use this event processor, create an instance of it
/// with all the dependencies it may need,
/// using Initialize(...dependencies).
///
/// This class contains a mapping from string
/// to IEffect, just like how EffectItemFactory contains
/// a mapping from that same string to EffectItem.
/// By having this foreign key relationship between
/// EffectItem and IEffect, we can decouple effect items
/// from their respective effects.
///
/// To create a new effect, create an implementation of
/// IEffect or ITimedEffect and place it in
/// Breakout.Entities.EffectItems.Effects.
/// Give the class a name that ends with "Effect".
/// </summary>
public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler _instance = null;
    private Dictionary<string, IEffect> _effects;

    private EffectItemHandler() {
        // We use the event type StatusEvent for effects
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
    }

    /// <summary>
    /// Inject dependencies and discover IEffect classes.
    /// Create a mapping from string to IEffect.
    /// </summary>
    public void Initialize(params object[] dependencies) {
        TypeLoader<IEffect> _effectLoader = new EffectLoader(dependencies);
        _effects = _effectLoader.CreateMapping();
    }

    public static EffectItemHandler GetInstance() {
        if (_instance == null) {
            _instance = new EffectItemHandler();
        }
        return _instance;
    }

    /// <summary>
    /// Activate or deactivate an effect
    /// based on its event's message.
    /// </summary>
    public void ProcessEvent(GameEvent gameEvent) {
        string fk = GetForeignKey(gameEvent.Message);
        IEffect effect = _effects[fk];
        if (gameEvent.Message.Contains("Deactivate")) {
            ((ITimedEffect) effect).Deactivate();
        }
        else {
            effect.Activate();
        }
    }

    /// <summary>
    /// Convert a status event message to a foreign key.
    /// A foreign key is a shared property between two objects.
    /// In this case, our effect items and effects are linked
    /// by name, so the foreign key is a string such as "ExtraLife".
    /// </summary>
    private string GetForeignKey(string message) {
        if (message.Contains("Deactivate")) {
            return message.Split("Deactivate")[0];
        }
        return message;
    }
}