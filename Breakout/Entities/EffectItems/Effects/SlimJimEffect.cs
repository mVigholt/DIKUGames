namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Timers;


/// <summary>Makes the shuttle wider for some time</summary>
public class SlimJimEffect : WideEffect {

    public SlimJimEffect(Shuttle shuttle) 
        : base(shuttle, extraWidth: -0.1f) {
    }
    
    public SlimJimEffect(Shuttle shuttle, float extraWidth)
        : base(shuttle, extraWidth) {
    }
}