namespace Breakout.Entities.EffectItems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DIKUArcade.Events;
using DIKUArcade.Entities;


public abstract class TypeLoader<T> {

    private string _namespace;

    public TypeLoader(string nameSpace) {
        _namespace = nameSpace;
    }

    /// <summary>Get a list of types in a namespace</summary>
    public List<Type> DiscoverTypes() {
        // Thanks to SO user aku
        // https://stackoverflow.com/a/79738
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == _namespace)
            .ToList();
    }

    public Dictionary<EffectItemType, T> CreateMapping() {
        var mapping = new Dictionary<EffectItemType, T>();
        foreach (Type t in DiscoverTypes()) {
            if (typeof(T).IsAssignableFrom(t)) {
                T instance = CreateInstance(t);
                if (instance != null) {
                    EffectItemType foreignKey = GetForeignKey(instance);
                    mapping[foreignKey] = instance;
                }
            }
        }
        return mapping;
    }

    public EffectItemType GetForeignKey(T instance) {
        PropertyInfo eiType = instance.GetType().GetProperty("Type");
        if (eiType != null && eiType.PropertyType == typeof(EffectItemType)) {
            EffectItemType itemType = (EffectItemType)eiType.GetValue(instance);
            return itemType;
        }
        throw new ArgumentException(
            $"Could not find the EffectItemType for {instance}"
        );
    }

    public abstract T CreateInstance(Type type);
}