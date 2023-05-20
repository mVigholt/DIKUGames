namespace Breakout.Entities.EffectItems.ItemConfigs;


public class ExtraPointsConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.ExtraPoints;

    public string IconFileName { get; } = "heart_filled.png";
    public bool IsTimed { get; } = false;
} 