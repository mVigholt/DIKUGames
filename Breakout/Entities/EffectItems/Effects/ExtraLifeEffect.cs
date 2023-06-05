namespace Breakout.Entities.EffectItems.Effects;

using Breakout.GameStates;


/// <summary>Gain an extra life</summary>
public class ExtraLifeEffect : IEffect {

    public void Activate() {
        GameRunning.GetInstance().LoseLives(-1);
    }
}