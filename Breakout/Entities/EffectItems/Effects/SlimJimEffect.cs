namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Makes the shuttle wider for some time</summary>
public class SlimJimEffect : WideEffect {

    public SlimJimEffect(Shuttle shuttle) 
        : base(shuttle, pctOfShuttleWidth: -0.5f) {
    }
    
    public SlimJimEffect(Shuttle shuttle, float pctOfShuttleWidth)
        : base(shuttle, pctOfShuttleWidth) {
    }
}