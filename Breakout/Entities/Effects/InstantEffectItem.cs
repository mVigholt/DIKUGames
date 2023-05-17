namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

public class InstantEffectItem : EffectItem
{
    public override GameEvent ActivationEvent { get; }

    public InstantEffectItem(DynamicShape shape, IBaseImage image, GameEvent activationEvent)
        : base(shape, image, activationEvent)
    {
        ActivationEvent = activationEvent;
    }

    public class Builder {
        private Vec2F _pos;
        private Vec2F _extent;
        private IBaseImage _image;
        private GameEvent _event;

        public Builder WithPosition(Vec2F pos) {
            _pos = pos;
            return this;
        }

        public Builder WithExtent(Vec2F extent) {
            _extent = extent;
            return this;
        }

        public Builder WithImage(IBaseImage image) {
            _image = image;
            return this;
        }

        // public Builder WithEffect
    }
}