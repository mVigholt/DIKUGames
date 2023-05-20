namespace Breakout.Entities.EffectItems.ItemConfigs;


public class ExtraBallsConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.ExtraBalls;

    public string IconFileName { get; } = "ExtraBallPowerUp.png";
    public bool IsTimed { get; } = false;
} 