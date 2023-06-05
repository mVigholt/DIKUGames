namespace Breakout.Entities.EffectItems.Effects;


/// <summary>
/// Give the player a higher speed for a duration of time
/// </summary>
public class PlayerSpeedEffect : SlownessEffect {

    public PlayerSpeedEffect(Shuttle shuttle) : base(shuttle, 1.5f) {
    }

    public PlayerSpeedEffect(Shuttle shuttle, float scalar) : base(shuttle, scalar) {
    }
}