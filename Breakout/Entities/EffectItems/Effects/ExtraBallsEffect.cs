namespace Breakout.Entities.EffectItems.Effects;

using DIKUArcade.Entities;
using Breakout.Entities;


// This one is not properly implemented yet
public class ExtraBallsEffect : IEffect {
        
    private EntityContainer<Ball> _activeBalls;
    private Shuttle _shuttle;

    public ExtraBallsEffect(EntityContainer<Ball> activeBalls, Shuttle shuttle) {
        _activeBalls = activeBalls;
        _shuttle = shuttle;
    }
    
    public void Activate() {
        _activeBalls.AddEntity(Ball.At(_shuttle.GetPosition()));
    }
}