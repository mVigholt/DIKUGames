namespace Breakout.Entities.EffectItems.ItemConfigs.Hazards;


public class SlowDownConfig : IEffectItemConfig {
    public EffectItemType Type { get; } = EffectItemType.SlowDown;

    public string IconFileName { get; } = "Slowness.png";
    public bool IsTimed { get; } = true;
} 