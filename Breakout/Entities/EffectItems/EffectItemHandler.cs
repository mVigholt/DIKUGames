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
        _shuttle = shuttle;
        _scoreBoard = scoreBoard;
        _activeBalls = activeBalls;
        _effects = new Dictionary<EffectItemType, IEffect>();
        List<Type> effectTypes = DiscoverTypes();
        foreach (Type t in effectTypes) {
            if (typeof(IEffect).IsAssignableFrom(t))
            {
                IEffect instance = CreateEffectInstance(t);
                if (instance != null)
                {
                    _effects[instance.Type] = instance;
                }
            }
        }
    }

    /// <summary>
    /// Instantiate an IEffect without knowing its type and dependencies
    /// </summary>
    private IEffect CreateEffectInstance(Type effectType) {
        ConstructorInfo[] constructors = effectType.GetConstructors();
        ConstructorInfo constructor = constructors.FirstOrDefault();
        if (constructor != null) {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++) {
                ParameterInfo parameter = parameters[i];
                if (parameter.ParameterType == typeof(Shuttle)) {
                    args[i] = _shuttle;
                }
                else if (parameter.ParameterType == typeof(ScoreBoard)) {
                    args[i] = _scoreBoard;
                }
                else if (parameter.ParameterType == typeof(EntityContainer<Ball>)) {
                    args[i] = _activeBalls;
                }
            }
            return (IEffect)constructor.Invoke(args);
        }
        return null;
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

    private EffectItemType GetEffectItemType(string message) {
        if (message.Contains("Deactivate")) {
            return GetEffectItemType(message.Split("Deactivate")[0]);
        }
        EffectItemType type;
        Enum.TryParse<EffectItemType>(message, out type);
        return type;
    }
}