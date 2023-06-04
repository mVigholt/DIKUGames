namespace Breakout.Entities.EffectItems.Effects;

using Breakout.GameStates;


public class ExtraLifeEffect : IEffect {

    public void Activate() {
        GameRunning.GetInstance().LoseLives(-1);
    }
}