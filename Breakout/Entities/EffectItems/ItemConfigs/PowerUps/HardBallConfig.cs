namespace Breakout.Entities.EffectItems.ItemConfigs.PowerUps;


public class HardBallConfig : IEffectItemConfig {
    public string IconFileName { get; } = "WallPowerUp.png";
    public bool IsTimed { get; } = true;
}