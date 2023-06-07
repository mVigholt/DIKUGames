namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Makes the shuttle wider for some time</summary>
public class WideEffect : ITimedEffect {

    private readonly float MIN_WIDTH = 0.05f;

    private Shuttle _shuttle;
    private float _extraWidth;

    public TimePeriod Duration { get; }

    // For testing
    public float ExtraWidth { get { return _extraWidth; } }

    public WideEffect(Shuttle shuttle) 
        : this(shuttle, extraWidth: 0.1f) {
    }

    public WideEffect(Shuttle shuttle, float extraWidth) {
        _shuttle = shuttle;
        Duration = TimePeriod.NewSeconds(5);
        _extraWidth = extraWidth;
        if (_shuttle.GetExtent().X + _extraWidth <= MIN_WIDTH) {
            _extraWidth = MIN_WIDTH;
        }
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