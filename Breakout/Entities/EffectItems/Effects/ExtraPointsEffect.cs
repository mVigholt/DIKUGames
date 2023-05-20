namespace Breakout.Entities.EffectItems.Effects;


// An easily tested powerup
public class ExtraPointsEffect : IEffect {
    
    public EffectItemType Type { get { return _type; } }

    private EffectItemType _type = EffectItemType.ExtraPoints;
    private ScoreBoard _scoreBoard;

    public ExtraPointsEffect(ScoreBoard scoreBoard) {
        _scoreBoard = scoreBoard;
    }

    public void Activate() {
        _scoreBoard.AddPoints(50);
        System.Console.WriteLine("PowerUp: ExtraPoints");
    }
}