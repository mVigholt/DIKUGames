namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.Events;

public abstract class EffectItem : MoveableEntity
{
    public abstract GameEvent ActivationEvent { get; }

    // Todo: Should not receive argument ev
    public EffectItem(DynamicShape shape, IBaseImage image, GameEvent ev)
        : base(shape, image) {
        }

    protected GameEvent CreateEvent(string message) {
        return new EventBuilder()
            .WithType(GameEventType.StatusEvent)
            .WithMessage(message)
            .Build();
    }
}