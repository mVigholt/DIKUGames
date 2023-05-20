namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Removes seconds from the game timer</summary>
public class LessTimeEffect : IEffect {

    public EffectItemType Type { get { return _type; } }

    private EffectItemType _type = EffectItemType.LessTime;

    public void Activate() {
        System.Console.WriteLine("Hazard: Less time");
    }
}