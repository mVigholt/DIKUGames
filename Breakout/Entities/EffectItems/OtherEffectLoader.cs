namespace Breakout.Entities.EffectItems;

using System;
using System.Reflection;


public class OtherEffectLoader : TypeLoader<IEffect> {

    private object _dependency;
    public OtherEffectLoader(object dependency) : base(
        "Breakout.Entities.EffectItems.Effects"
    ) {
        _dependency = dependency;
        var types = DiscoverTypes();
        System.Console.WriteLine("From OEF:");
        types.ForEach((i) => Console.WriteLine(i));
    }

    public override IEffect CreateInstance(Type type) {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor != null) {
            Console.WriteLine("Creating instance");
            return (IEffect)Activator.CreateInstance(type);
        }
        return null;
    }
}