namespace Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {
    private float speed = 0.0003f;
    public float Speed{
        get{
            return speed;
        }
        private set{
            speed = value;
        }
    }

    private static readonly Vec2F EXTENT = new Vec2F(0.1f, 0.1f);

    public readonly Vec2F startPosition;

    private IBaseImage alterImage;

    private int hitpoints = 4;
    public int Hitpoints{
        get {
            return hitpoints;
        }
        set {
            hitpoints = value;
        }
    }

    public Enemy(Vec2F position, IBaseImage image, IBaseImage alterImage)
        :base(new DynamicShape(position, EXTENT), image) {
        this.Image = image;
        this.alterImage = alterImage;
        this.startPosition = position;
    }

    public bool isEnraged(){
        if (this.Hitpoints <= 2) {
            this.Speed += 0.0001f;
            this.Image = alterImage;
            return true;
        }
        else {
            return false;
        }
    }
}