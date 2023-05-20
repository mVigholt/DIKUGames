namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Slows down the shuttle</summary>
public class SlowDownEffect : ITimedEffect {

    public EffectItemType Type { get { return _type; } }
    public TimePeriod TimeLeft { get; }

    private EffectItemType _type = EffectItemType.SlowDown;
    private Shuttle _shuttle;
    private readonly float SCALAR = 0.5f;
    
    public SlowDownEffect(Shuttle shuttle) {
        _shuttle = shuttle;
        TimeLeft = TimePeriod.NewSeconds(3);
    }

    public void Activate() {
        _shuttle.Speed *= SCALAR;
        System.Console.WriteLine("PowerUp: SlowDown");
    }

    public void Deactivate() {
        _shuttle.Speed /= SCALAR;
        System.Console.WriteLine("PowerUp: SlowDown deactivated");
    }
}