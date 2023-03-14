namespace Galaga;

using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public class Enemy : Entity {
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
    public Enemy(DynamicShape shape, IBaseImage image, IBaseImage alterImage)
        :base(shape,image) {
        this.Image = image;
        this.alterImage = alterImage;
    }

    public bool isEnraged(){
        if (this.Hitpoints <= 2){
            this.Image = alterImage;
            return true;
        }
        else {
            return false;
        }
    }
}