namespace Breakout.Entities.EffectItems.ItemConfigs.Hazards;


public class SlownessConfig : IEffectItemConfig {
    public string IconFileName { get; } = "Slowness.png";
    public bool IsTimed { get; } = true;
} 