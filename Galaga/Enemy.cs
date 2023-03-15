namespace Galaga;

using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Enemy : Entity {
    private float speed = 0.001f;
    public float Speed{
        get{
            return speed;
        }
        private set{
            speed = value;
        }
    }
    private static Vec2F extend = new(0.1f, 0.1f);
    // public Vec2F startPosition {get;}
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
        :base(new DynamicShape(position, extend), image)  {
        this.Image = image;
        this.alterImage = alterImage;
        // this.startPosition = position;
    }

    public bool isEnraged(){
        if (this.Hitpoints <= 2){
            this.Speed += 0.0008f;
            this.Image = alterImage;
            return true;
        }
        else {
            return false;
        }
    }
}