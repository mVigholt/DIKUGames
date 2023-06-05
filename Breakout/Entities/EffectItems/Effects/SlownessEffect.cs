namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Slows down the shuttle</summary>
public class SlownessEffect : ITimedEffect {

    public TimePeriod TimeLeft { get; }

    private Shuttle _shuttle;
    private float _scalar;
    
    public SlownessEffect(Shuttle shuttle)
        : this(shuttle, 0.5f) {
    }

    public SlownessEffect(Shuttle shuttle, float scalar) {
        _scalar = scalar;
        _shuttle = shuttle;
        TimeLeft = TimePeriod.NewSeconds(3);
    }

    public void Activate() {
        _shuttle.Speed *= _scalar;
    }

    public void Deactivate() {
        _shuttle.Speed /= _scalar;
    }
}