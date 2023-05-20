namespace Breakout.Entities.EffectItems;

using System;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;

/// <summary>
/// Listens for events of type GameEventType.StatusEvent.
/// Those are used for power-ups and hazards, AKA effect items.
/// To use this event processor, create an instance of it
/// with all the dependencies it may need,
/// using Initialize(...dependencies).
/// 
/// This class contains a mapping from EffectItemType
/// to IEffect, just like how EffectItemFactory contains
/// a mapping from EffectItemType to EffectItem.
/// By having this foreign key relationship between
/// EffectItem and IEffect,we can decouple effect items
/// from their respective effects.
/// 
/// To create a new effect, create an implementation of
/// IEffect or ITimedEffect and place it in
/// Breakout.Entities.EffectItems.Effects.
/// Give it the same EffectItemType as its corresponding
/// EffectItem, the physical entity that has an image and a position.
/// </summary>
public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler _instance = null;
    private Dictionary<EffectItemType, IEffect> _effects;

    /// <summary>
    /// Inject dependencies and discover IEffect classes.
    /// Create a mapping from EffectItemType to IEffect.
    /// </summary>
    public void Initialize(
        Shuttle shuttle,
        ScoreBoard scoreBoard,
        EntityContainer<Ball> activeBalls
    ) {
        var effectLoader = new EffectLoader(shuttle, scoreBoard, activeBalls);
        _effects = effectLoader.CreateMapping();
    }

    public static EffectItemHandler GetInstance() {
        if (_instance == null) {
            _instance = new EffectItemHandler();
        }
        return _instance;
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

    /// <summary>
    /// Convert a string to an EffectItemType, or null if unsuccessful
    /// </summary>
    private EffectItemType GetEffectItemType(string message) {
        if (message.Contains("Deactivate")) {
            return GetEffectItemType(message.Split("Deactivate")[0]);
        }
        EffectItemType type;
        Enum.TryParse<EffectItemType>(message, out type);
        return type;
    }
}