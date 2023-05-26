namespace Breakout.Entities.EffectItems;

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Breakout.Entities.Board;
using DIKUArcade.Entities;

public class EffectLoader : TypeLoader<IEffect> {

    private Dictionary<Type, object> dependencies;

    public EffectLoader(params object[] dependencies)
        : base("Breakout.Entities.EffectItems.Effects") {
        this.dependencies = new Dictionary<Type, object>();
        foreach (object d in dependencies) {
            this.dependencies.Add(d.GetType(), d);
        }
    }

    public override IEffect CreateInstance(Type type) {
        ConstructorInfo[] constructors = type.GetConstructors();
        ConstructorInfo constructor = constructors.FirstOrDefault();
        if (constructor != null) {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++) {
                ParameterInfo parameter = parameters[i];
                if (dependencies.TryGetValue(parameter.ParameterType, out object dependency)) {
                    args[Array.IndexOf(parameters, parameter)] = dependency;
                }
                else {
                    throw new ArgumentException(
                        $"Could not create an instance of type {type}"
                    );
                }
            }
            return (IEffect)constructor.Invoke(args);
        }
        return null;
    }
}