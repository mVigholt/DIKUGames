 namespace Breakout.Levels;

using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Events;
using Breakout.Entities.EffectItems;
using Breakout.Entities.Board;

public class EffectItemsLoader{
    private EntityContainer<Ball> balls;
    private EffectItemHandler effectItemHandler;
    private GameEventBus eventBus = GameBus.GetBus();
    public EffectItemsLoader(Shuttle shuttle, ScoreBoard scoreBoard){
        InitEffectItems(shuttle, scoreBoard);
    }



 public void InitEffectItems(Shuttle shuttle, ScoreBoard scoreBoard) {
        effectItemHandler = EffectItemHandler.GetInstance();
        effectItemHandler.Initialize(shuttle, scoreBoard, balls);
        GameBus.GetBus().Unsubscribe(GameEventType.StatusEvent, effectItemHandler);
        GameBus.GetBus().Subscribe(GameEventType.StatusEvent, effectItemHandler);
    }

 private void ActivateEffectItem(EffectItem item) {
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