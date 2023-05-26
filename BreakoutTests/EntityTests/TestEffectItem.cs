namespace BreakoutTests.EntityTests;

using System;
using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.EffectItems;

[TestFixture]
public class TestEffectItem {

    [SetUp]
    public void SetUp() {
        Window.CreateOpenGLContext();
    }

    private bool IsWithinBounds(MoveableEntity entity) {
        return entity.GetPosition().X <= 1.0f &&
                entity.GetPosition().X >= 0.0f;
    }

    [Test]
    public void TestEffectItemCollisionWithBottom() {
        // Effect items should disappear if they reach the bottom
        EffectItemFactory factory = new EffectItemFactory(false);
        Vec2F center = new Vec2F(0.5f, 0.5f);
        EffectItem item = factory.RandomPowerUp(center);
        EntityContainer<EffectItem> items = new EntityContainer<EffectItem>();
        items.AddEntity(item);
        // Precondition: EffectItem is not out of bounds
        IsWithinBounds(item);
        for (int i = 0; i < 100; i++) {
            items.Iterate(entity => {
                entity.Move();
            });
        }
        // Postcondition: Items have been deleted
        // because they went out of bounds
        Assert.That(item.IsDeleted());
        Assert.AreEqual(0, items.CountEntities());
    }
}