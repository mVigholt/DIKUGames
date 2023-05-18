namespace Breakout.Entities.Effects;


public interface IEffect {
    EffectItemType Type { get; }
    void Activate();
}