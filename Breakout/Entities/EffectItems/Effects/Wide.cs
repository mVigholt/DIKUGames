namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


public class Wide : ITimedEffect {

    private EffectItemType _type = EffectItemType.Wide;
    public TimePeriod TimeLeft { get; }

    public EffectItemType Type { get { return _type; } }
    private Shuttle _shuttle;

    public Wide(Shuttle shuttle) {
        _shuttle = shuttle;
        TimeLeft = TimePeriod.NewSeconds(5);
    }

    public void Activate() {
        System.Console.WriteLine("PowerUp: Wide");
    }

    public void Deactivate() {
        System.Console.WriteLine("PowerUp: Wide deactivated");
    }
}