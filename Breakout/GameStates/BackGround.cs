namespace Breakout.GameStates;

using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class BackGround: Entity{
    private IBaseImage BackGroundImage;
    private static readonly Vec2F BACKGROUND_POSITION = new Vec2F (0.0f, 0.0f);
    private static readonly Vec2F BACKGROUND_EXTENT = new Vec2F(1.0f, 1.0f);

    public BackGround(IBaseImage image) :
        base(new StationaryShape (BACKGROUND_POSITION,BACKGROUND_EXTENT), image){
        this.BackGroundImage = image;
    }

     public BackGround(Vec2F pos, Vec2F extent, IBaseImage image) :
        base(new StationaryShape (pos, extent), image){
        this.BackGroundImage = image;
    }

}