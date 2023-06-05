namespace Breakout.Entities.EffectItems.Effects;

using System.Collections.Generic;
using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Timers;


/// <summary>Makes the current ball destroy every block that it hits</summary>
public class HardBallEffect : ITimedEffect {

    private EntityContainer<Ball> _balls;
    private Ball _affectedBall;

    public TimePeriod Duration { get; }

    public HardBallEffect(EntityContainer<Ball> balls) {
        _balls = balls;
        Duration = TimePeriod.NewSeconds(3);
    }

    public void Activate() {
        Ball ball = FirstBall(_balls);
        if (ball != null) {
            ball.IsHard = true;
            _affectedBall = ball;
        }
    }

    public void Deactivate() {
        if (_affectedBall != null) {
            _affectedBall.IsHard = false;
        }
    }

    /// <summary>
    /// Return the first ball from the container,
    /// null if the container has no balls.
    /// </summary>
    private Ball FirstBall(EntityContainer<Ball> balls) {
        if (balls.CountEntities() == 0) {
            return null;
        }
        var enumerator = balls.GetEnumerator();
        enumerator.MoveNext();
        return enumerator.Current as Ball;
    }
}