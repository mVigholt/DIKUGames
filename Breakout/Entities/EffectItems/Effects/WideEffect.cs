namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Makes the shuttle wider for some time</summary>
public class WideEffect : ITimedEffect {

    public TimePeriod Duration { get; }

    private Shuttle _shuttle;
    private float _pctOfShuttleWidth;
    private float _deltaWidth;

    // For testing
    public float pctOfShuttleWidth { get { return _pctOfShuttleWidth; } }

    public WideEffect(Shuttle shuttle) 
        : this(shuttle, pctOfShuttleWidth: 0.5f) {
    }

    public WideEffect(Shuttle shuttle, float pctOfShuttleWidth) {
        _shuttle = shuttle;
        Duration = TimePeriod.NewSeconds(5);
        _pctOfShuttleWidth = pctOfShuttleWidth;
    }

    public void Activate() {
        float shuttleWidth = _shuttle.GetExtent().X;
        _deltaWidth = _pctOfShuttleWidth * shuttleWidth;
        _shuttle.Shape.Extent.X += _deltaWidth;
        _shuttle.Shape.Position.X -= _deltaWidth / 2f;
    }

    public void Deactivate() {
        float shuttleWidth = _shuttle.GetExtent().X;
        _shuttle.Shape.Extent.X -= _deltaWidth;
        _shuttle.Shape.Position.X += _deltaWidth / 2f;
    }
}