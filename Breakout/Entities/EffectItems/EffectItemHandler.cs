namespace Breakout.Entities.EffectItems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DIKUArcade.Events;
using DIKUArcade.Entities;

public class EffectItemHandler : IGameEventProcessor {

    private static EffectItemHandler _instance = null;
    private Dictionary<EffectItemType, IEffect> _effects;
    private Shuttle _shuttle;
    private ScoreBoard _scoreBoard;
    private EntityContainer<Ball> _activeBalls;

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

    /// <summary>Find all the classes in the Effects namespace</summary>
    private List<Type> DiscoverTypes() {
        // Thanks to SO user aku
        // https://stackoverflow.com/a/79738
        string ns = "Breakout.Entities.EffectItems.Effects";
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == ns)
            .ToList();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        Console.WriteLine($"ProcessEvent({gameEvent.Message})");
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