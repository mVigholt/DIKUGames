namespace Breakout.Entities.EffectItems;

using System;
using System.Reflection;
using Breakout.Entities.EffectItems.ItemConfigs;


public class EffectItemConfigLoader : TypeLoader<IEffectItemConfig> {

    public EffectItemConfigLoader(string nameSpace) : base(nameSpace) {
    }

    public override IEffectItemConfig CreateInstance(Type type) {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor != null) {
            return (IEffectItemConfig)Activator.CreateInstance(type);
        }
        return null;
    }
}