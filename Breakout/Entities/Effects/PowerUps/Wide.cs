namespace Breakout.Entities.Effects.PowerUps;

using DIKUArcade.Timers;


public class Wide : ITimedEffect {

    private EffectItemType _type = EffectItemType.Wide;
    public TimePeriod TimeLeft { get; }

    public EffectItemType Type { get { return _type; } }

    public Wide() {
        TimeLeft = TimePeriod.NewSeconds(5);
    }

    public void Activate() {
        System.Console.WriteLine("PowerUp: Wide");
    }

    public void Deactivate() {
        System.Console.WriteLine("PowerUp: Wide deactivated");
    }
}