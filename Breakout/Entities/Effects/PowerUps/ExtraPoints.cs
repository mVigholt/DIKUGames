namespace Breakout.Entities.Effects.PowerUps;


// An easily tested powerup
public class ExtraPoints : IInstantEffect {

    private ScoreBoard _scoreBoard;

    public ExtraPoints(ScoreBoard scoreBoard) {
        _scoreBoard = scoreBoard;
    }

    public void Activate() {
        _scoreBoard.AddPoints(50);
    }
}