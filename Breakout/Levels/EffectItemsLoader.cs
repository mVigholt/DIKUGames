namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Events;
using Breakout.Entities.EffectItems;
using Breakout.Entities.Board;

public class EffectItemsLoader {
    private EffectItemHandler effectItemHandler;
    private GameEventBus eventBus = GameBus.GetBus();
    public EffectItemsLoader(
            Shuttle shuttle,
            ScoreBoard scoreBoard,
            EntityContainer<Ball> balls) {
        effectItemHandler = EffectItemHandler.GetInstance();
        effectItemHandler.Initialize(shuttle, scoreBoard, balls);
        GameBus.GetBus().Unsubscribe(GameEventType.StatusEvent, effectItemHandler);
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, effectItemHandler);
    }
    
    public void ActivateEffectItem(EffectItem item) {
        if (item is InstantEffectItem instantItem) {
            eventBus.RegisterEvent(instantItem.ActivationEvent);
        }
        if (item is TimedEffectItem timedItem) {
            eventBus.RegisterEvent(timedItem.ActivationEvent);
            eventBus.RegisterTimedEvent(
                timedItem.DeactivationEvent,
                timedItem.TimeLeft
            );
        }
    }

}