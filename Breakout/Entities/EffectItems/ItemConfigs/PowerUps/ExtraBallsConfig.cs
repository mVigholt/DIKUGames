namespace Breakout.Entities.EffectItems.ItemConfigs.PowerUps;


public class ExtraBallsConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.ExtraBalls;

    public string IconFileName { get; } = "ExtraBallPowerUp.png";
    public bool IsTimed { get; } = false;
} 