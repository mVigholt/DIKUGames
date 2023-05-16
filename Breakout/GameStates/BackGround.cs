namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class BackGround: Entity{
    IBaseImage BackGroundImage;

    public BackGround(IBaseImage image) :
        base(new StationaryShape (new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)), image){
        this.BackGroundImage = image;
    }

}