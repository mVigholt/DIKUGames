using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Galaga;

public class PlayerShot : Entity{
    private static Vec2F extend = (Vec2F) (0.008f, 0.021f);
    private static Vec2F direction = (Vec2F) (0.0f, 0.1f);
    public PlayerShot(Vec2F position, IBaseImage image):
        base(new DynamicShape (position, extend, direction), image) {}
}


