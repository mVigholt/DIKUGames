namespace BreakoutTests.EntityTests.EffectItems;

using NUnit.Framework;
using DIKUArcade.Math;
using Breakout.Entities;
using Breakout.Entities.EffectItems;

[TestFixture]
public class TestEffectItem {

    private bool IsWithinBounds(MoveableEntity entity) {
        return entity.GetPosition().X <= 1.0f &&
                entity.GetPosition().X >= 0.0f;
    }

    [Test]
    public void TestEffectItemCollisionWithBottom() {
        // Effect items should disappear if they reach the bottom
        EffectItemFactory factory = new EffectItemFactory();
        Vec2F center = new Vec2F(0.5f, 0.5f);
        EffectItem item = factory.RandomPowerUp(center);
        // Precondition: EffectItem is not out of bounds
        Assert.That(IsWithinBounds(item));
        for (int i = 0; i < 1000; i++) {
            item.Move();
        }
        // Postcondition: Items have been deleted
        // because they went out of bounds
        Assert.That(item.IsDeleted());
    }

    [Test]
    public void EffectItemHasConstantSpeed() {
        EffectItemFactory factory = new EffectItemFactory();
        Vec2F center = new Vec2F(0.5f, 0.8f);
        EffectItem item = factory.RandomPowerUp(center);
        // Precondition: EffectItem is not out of bounds
        Assert.That(IsWithinBounds(item));
        // Postcondition: EffectItem moves with constant speed
        for (int i = 0; i < 2; i++) {item.Move();}
        Vec2F deltaPosA = center - item.GetPosition();
        for (int i = 0; i < 2; i++) {item.Move();}
        Vec2F deltaPosB = center - item.GetPosition();
        Assert.That(IsWithinBounds(item));
        Assert.AreEqual(
            deltaPosA.Y,
            deltaPosB.Y / 2  
        );
    }
}