namespace Breakout.Entities.EffectItems;

using System;
using System.Linq;
using System.Reflection;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;
using Breakout.IO;
using Breakout.Events;
using Breakout.Entities.EffectItems.ItemConfigs;


public class EffectItemConfigLoader : TypeLoader<IEffectItemConfig> {

    // private readonly Vec2F STD_EXTENT = new Vec2F(0.05f, 0.05f);

    public EffectItemConfigLoader(string nameSpace) : base(nameSpace) {
    }

    public override IEffectItemConfig CreateInstance(Type type) {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor != null) {
            return (IEffectItemConfig)Activator.CreateInstance(type);
        }
        return null;
    }

    // private GameEvent CreateEvent(string msg) {
    //     return new EventBuilder()
    //         .WithType(GameEventType.StatusEvent)
    //         .WithMessage(msg)
    //         .Build();
    // }

    // private InstantEffectItem CreateInstantEffectItem(
    //     Vec2F pos,
    //     string imageFilename,
    //     EffectItemType type
    // ) {
    //     DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
    //     Image image = Assets.LoadImage(imageFilename);
    //     GameEvent ev = CreateEvent(type.ToString());
    //     return new InstantEffectItem(type, shape, image, ev);
    // }

    // private TimedEffectItem CreateTimedEffectItem(
    //     Vec2F pos,
    //     string imageFilename,
    //     EffectItemType type
    // ) {
    //     DynamicShape shape = new DynamicShape(pos, STD_EXTENT);
    //     Image image = Assets.LoadImage(imageFilename);
    //     GameEvent activationEvent = CreateEvent(type.ToString());
    //     EffectItemType deactivationType;
    //     Enum.TryParse<EffectItemType>(activationEvent.Message, out deactivationType);
    //     GameEvent deactivationEvent = CreateEvent(deactivationType.ToString() + "Deactivate");
    //     TimePeriod duration = TimePeriod.NewSeconds(5);
    //     return new TimedEffectItem(
    //         type, shape, image, activationEvent, deactivationEvent, duration
    //     );
    // }
}