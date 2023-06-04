namespace BreakoutTests.EntityTests.EffectItems.Effects;

using DIKUArcade.Events;
using Breakout.Events;
using Breakout.Entities.EffectItems;


/// <summary>
/// Static helper class that can activate or
/// deactivate an event of type 
/// GameStateEvent.StatusEvent.
/// </summary>
public static class EffectSimulator {

    public static void Activate(string effectName) {
        GameEvent ev = new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(effectName)
            .Build();
        EffectItemHandler.GetInstance().ProcessEvent(ev);
    }

    public static void Deactivate(string effectName) {
        GameEvent ev = new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage($"{effectName} Deactivate")
            .Build();
        EffectItemHandler.GetInstance().ProcessEvent(ev);
    }
}