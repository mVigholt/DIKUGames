namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Makes the shuttle wider for some time</summary>
public class WideEffect : ITimedEffect {

    public TimePeriod Duration { get; }

    private Shuttle _shuttle;
    private float _extraWidth;

    // For testing
    public float ExtraWidth { get { return _extraWidth; } }

    public WideEffect(Shuttle shuttle) {
        _shuttle = shuttle;
        Duration = TimePeriod.NewSeconds(5);
        _extraWidth = 0.1f;
    }

    public void Activate() {
        _shuttle.Shape.Extent.X += _extraWidth;
        _shuttle.Shape.Position.X -= _extraWidth / 2f;
    }

    public void Deactivate() {
        _shuttle.Shape.Extent.X -= _extraWidth;
        _shuttle.Shape.Position.X += _extraWidth / 2f;
    }
}