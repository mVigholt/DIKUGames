namespace Breakout.Entities.EffectItems.ItemConfigs.Hazards;


public class LessTimeConfig : IEffectItemConfig {
    public string IconFileName { get; } = "HalfSpeedPowerUp.png";
    public bool IsTimed { get; } = false;
} 