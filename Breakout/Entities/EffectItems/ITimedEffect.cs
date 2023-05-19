namespace Breakout.Entities.EffectItems;

using DIKUArcade.Timers;


public interface ITimedEffect : IEffect {
    void Deactivate();
    TimePeriod TimeLeft { get; }
}