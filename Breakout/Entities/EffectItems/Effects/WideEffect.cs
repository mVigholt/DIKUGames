namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


public class WideEffect : ITimedEffect {

    public TimePeriod TimeLeft { get; }

    private Shuttle _shuttle;

    public WideEffect(Shuttle shuttle) {
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