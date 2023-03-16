namespace Galaga;

using System;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;

public class Health {

    private int points;
    private int max;
    private Text display;
    private Entity owner;

    public Health (Entity owner, int startingHealth) {
        Vec2F pos = owner.Shape.Position;
        Vec2F extent = new Vec2F(0.3f, 0.3f);
        this.owner = owner;
        points = startingHealth;
        max = startingHealth;
        display = new Text($"{points}", pos, extent);
        display.SetColor(new Vec3I(255,0,0));
    }

    public int Points {
        get { return points; }
    }

    public int Max {
        get { return max; }
    }

    // Remember to explaination your choice as to what happens
    // when losing health.
    public void LoseHealth() {
        points--;
        display.SetText($"{points}");
    }

    public void Render() {
        // Something weird is going on with the positioning, hence the magic numbers.
        // It seems to be a bug in DIKUArena, but I could be wrong.
        // See the comment at DIKUArcade.Graphics.Text line 95.
        display.GetShape().Position = new Vec2F(
            owner.Shape.Position.X + owner.Shape.Extent.X / 2.55f,
            owner.Shape.Position.Y - 0.14f
        );
        display.RenderText();
    }
}