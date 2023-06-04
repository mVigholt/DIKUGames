namespace Breakout.Entities.EffectItems.ItemConfigs.PowerUps;


public class ExtraLifeConfig : IEffectItemConfig {
    public string IconFileName { get; } = "heart_filled.png";
    public bool IsTimed { get; } = false;
} 