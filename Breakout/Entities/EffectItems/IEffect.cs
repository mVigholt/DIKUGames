namespace Breakout.Entities.EffectItems;


public interface IEffect {
    EffectItemType Type { get; }
    void Activate();
}