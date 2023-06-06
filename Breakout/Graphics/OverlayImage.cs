namespace Breakout.Graphics;

using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.IO;


public class OverlayImage : IBaseImage {

    private Texture _base;
    private Texture _overlay;
    private Vec2F _overlayScale = new Vec2F(1f / 3f, 3f / 4f);

    public Vec2F OverlayScale {
        get { return _overlayScale; }
        set { _overlayScale = value; }
    }

    public OverlayImage(string baseImageFile, string overlayImageFile) {
        var PathTo = (string file) => Path.Combine(PathFinder.Images(), file);
        _base = new Texture(PathTo(baseImageFile));
        _overlay = new Texture(PathTo(overlayImageFile));
    }

    public OverlayImage(Texture baseTexture, Texture overlayTexture) {
        _base = baseTexture;
        _overlay = overlayTexture;
    }

    public OverlayImage(IBaseImage image, IBaseImage powerUpImage) {
        _base = ((Image)image).GetTexture();
        _overlay = ((Image)powerUpImage).GetTexture();
    }

    public void Render(Shape shape) {
        _base.Render(shape);
        Shape overlayShape = ScaleShape(shape);
        _overlay.Render(overlayShape);
    }

    public void Render(Shape shape, Camera camera) {
        _base.Render(shape, camera);
        Shape overlayShape = ScaleShape(shape);
        _overlay.Render(shape, camera);
    }

    private Shape ScaleShape(Shape shape) {
        Shape newShape = new Shape();
        newShape.Position = new Vec2F(shape.Position.X, shape.Position.Y);
        newShape.Extent = new Vec2F(shape.Extent.X, shape.Extent.Y);
        newShape.ScaleXFromCenter(_overlayScale.X);
        newShape.ScaleYFromCenter(_overlayScale.Y);
        return newShape;
    }
}