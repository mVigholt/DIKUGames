namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Slows down the shuttle</summary>
public class LessTime : IEffect {

    public EffectItemType Type { get { return _type; } }

    private EffectItemType _type = EffectItemType.LessTime;

    public void Activate() {
        System.Console.WriteLine("Hazard: Less time");
    }
}