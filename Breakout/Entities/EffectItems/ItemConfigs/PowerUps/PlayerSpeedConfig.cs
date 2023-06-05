namespace Breakout.Entities.EffectItems.ItemConfigs.PowerUps;


public class PlayerSpeedConfig : IEffectItemConfig {
    public string IconFileName { get; } = "DoubleSpeedPowerUp.png";
    public bool IsTimed { get; } = true;
}