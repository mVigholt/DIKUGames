namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class PlayerShot : Entity {
    private static Vec2F extend = new(0.008f, 0.021f);
    private static Vec2F direction = new(0.0f, 0.1f);
    public PlayerShot(Vec2F position, IBaseImage image) :
        base(new DynamicShape(position, extend, direction), image) {
    }
}


