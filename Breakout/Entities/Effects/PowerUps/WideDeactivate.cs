namespace Breakout.Entities.Effects.PowerUps;

using DIKUArcade.Timers;


public class WideDeactivate : IEffect {

    private EffectItemType _type = EffectItemType.WideDeactivate;

    public EffectItemType Type { get { return _type; } }

    public void Activate() {
        System.Console.WriteLine("PowerUp: WideDeactivate");
    }
}