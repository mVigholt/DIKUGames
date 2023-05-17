namespace Breakout.Entities.Effects.PowerUps;


// An easily tested powerup
public class ExtraPoints : IEffect {

    // primary key
    private EffectItemType _type = EffectItemType.ExtraPoints;
    private ScoreBoard _scoreBoard;
    public EffectItemType Type { get { return _type; } }
    

    public ExtraPoints(ScoreBoard scoreBoard) {
        _scoreBoard = scoreBoard;
    }

    public void Activate() {
        _scoreBoard.AddPoints(50);
    }
}