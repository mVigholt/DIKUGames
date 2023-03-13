namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Enemy : Entity {
    private int hitpoints;
    private IBaseImage image;
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape,image) {
    }
}