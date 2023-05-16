namespace Breakout.Entities.Effects;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.IO;

public interface IEffectItemCreator {
    EffectItem Create();
}