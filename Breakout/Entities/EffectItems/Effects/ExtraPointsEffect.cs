namespace Breakout.Entities.EffectItems.Effects;

using Breakout.Entities.Board;


// An easily tested powerup
public class ExtraPointsEffect : IEffect {

    private ScoreBoard _scoreBoard;

    public ExtraPointsEffect(ScoreBoard scoreBoard) {
        _scoreBoard = scoreBoard;
    }

    public void Activate() {
        _scoreBoard.AddPoints(50);
        System.Console.WriteLine("PowerUp: ExtraPoints");
    }
}