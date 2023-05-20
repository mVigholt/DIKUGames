namespace Breakout.Entities.EffectItems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


/// <summary>
/// This generic class can load classes from a namespace.
/// Given a namespace upon instantiation,
/// TypeLoader<T>.CreateMapping() creates a mapping from
/// string to T, where the type T is a class
/// in the given namespace.
/// Subclasses must implement the method
/// CreateInstance, which takes a class from the namespace
/// as input and returns an instance of it.
/// </summary>
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

    /// <summary>
    /// Create a mapping from string to
    /// instances of type T. string is shared
    /// between EffectItems and IEffects, so it works
    /// as a foreign key.
    /// </summary>
    public Dictionary<string, T> CreateMapping() {
        var mapping = new Dictionary<string, T>();
        foreach (Type t in DiscoverTypes()) {
            if (typeof(T).IsAssignableFrom(t)) {
                T instance = CreateInstance(t);
                if (instance != null) {
                    string foreignKey = GetForeignKey(instance);
                    mapping[foreignKey] = instance;
                }
            }
        }
        return mapping;
    }

    /// <summary>Get the string associated with an instance of T</summary>
    public string GetForeignKey(T instance) {
        string className = instance.GetType().Name;
        string[] suffixes = new string[]{ "Effect", "Config" };
        foreach (string suffix in suffixes) {
            if (className.EndsWith(suffix)) {
                return className.Substring(0, className.Length - suffix.Length);
            }
        }
        throw new ArgumentException(
            $"Could not get the foreign key for {className}." +
            "It needs to end with \"Effect\" or \"Config\"."
        );
        // PropertyInfo eiType = instance.GetType().GetProperty("Type");
        // if (eiType != null && eiType.PropertyType == typeof(string)) {
        //     string itemType = (string)eiType.GetValue(instance);
        //     return itemType;
        // }
        // throw new ArgumentException(
        //     $"Could not find the string for {instance}"
        // );
    }

    public abstract T CreateInstance(Type type);
}