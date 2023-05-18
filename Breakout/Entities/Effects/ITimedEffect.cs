namespace Breakout.Entities.Effects;

using DIKUArcade.Timers;


public interface ITimedEffect : IEffect {
    void Deactivate();
    TimePeriod TimeLeft { get; }
}