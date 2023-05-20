namespace Breakout.Entities.EffectItems.ItemConfigs;


public class LessTimeConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.LessTime;

    public string IconFileName { get; } = "HalfSpeedPowerUp.png";
    public bool IsTimed { get; } = false;
} 