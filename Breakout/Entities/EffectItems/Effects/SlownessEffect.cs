namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;
using DIKUArcade.Events;
using Breakout.Events;


/// <summary>Slows down the shuttle for a duration of time</summary>
public class SlownessEffect : ITimedEffect {

    private Shuttle _shuttle;
    private float _scalar;

    public TimePeriod Duration { get; }
    
    public SlownessEffect(Shuttle shuttle)
        : this(shuttle, 0.5f) {
    }

    public SlownessEffect(Shuttle shuttle, float scalar) {
        _scalar = scalar;
        _shuttle = shuttle;
        Duration = TimePeriod.NewSeconds(3);
    }

    public void Activate() {
        _shuttle.Speed *= _scalar;
        _shuttle.UpdateDirection(_shuttle.GetDirection());
    }

    public void Deactivate() {
        _shuttle.Speed /= _scalar;
        _shuttle.UpdateDirection(_shuttle.GetDirection());
    }
}