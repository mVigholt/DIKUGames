namespace Galaga;

using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Enemy : Entity {
    private int hitpoints = 4;
    public int Hitpoints{
        get {
            return hitpoints;
        }
        set {
            hitpoints = value;
        }
    }
    private IBaseImage image;
    public Enemy(DynamicShape shape, IBaseImage image) : base(shape,image) {
        this.Image = image;
    }
}