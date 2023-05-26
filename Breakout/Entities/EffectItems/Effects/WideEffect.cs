namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;
using DIKUArcade.Math;

public class WideEffect : ITimedEffect {

    public TimePeriod TimeLeft { get; }

    private Shuttle _shuttle;
    private Vec2F extraWidth = new Vec2F(0.1f, 0f);

    public WideEffect(Shuttle shuttle) {
        _shuttle = shuttle;
        TimeLeft = TimePeriod.NewSeconds(5);
    }

    public void Activate() {
        _shuttle.Shape.Extent += extraWidth;
        
    }

    public void Deactivate() {
        _shuttle.Shape.Extent -= extraWidth;
    }
}