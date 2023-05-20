namespace Breakout.Entities.EffectItems.ItemConfigs;


public class WideConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.Wide;

    public string IconFileName { get; } = "WidePowerUp.png";
    public bool IsTimed { get; } = true;
}