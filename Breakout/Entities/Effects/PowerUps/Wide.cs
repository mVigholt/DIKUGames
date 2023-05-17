namespace Breakout.Entities.Effects.PowerUps;

using DIKUArcade.Timers;


// An easily tested powerup
public class Wide : ITimedEffect {

    private ScoreBoard _scoreBoard;
    private EffectItemType _type = EffectItemType.Wide;

    public TimePeriod TimeLeft { get; }

    public EffectItemType Type { get { return _type; } }

    public Wide(TimePeriod timeLeft) {
        TimeLeft = timeLeft;
    }

    public void Activate() {
        System.Console.WriteLine("Activated Wide");
    }

    public void Deactivate() {
        System.Console.WriteLine("Deactivated Wide");
    }
}