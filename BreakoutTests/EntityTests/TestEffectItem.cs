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
        // Precondition: EffectItem is not out of bounds
        IsWithinBounds(item);
        for (int i = 0; i < 1000; i++) {
            item.Move();
        }
        // Postcondition: Items have been deleted
        // because they went out of bounds
        Console.WriteLine("Position: " + item.GetPosition());
        Assert.That(item.IsDeleted());
    }

    [Test]
    public void EffectItemHasConstantSpeed() {
        EffectItemFactory factory = new EffectItemFactory(false);
        Vec2F center = new Vec2F(0.5f, 0.5f);
        EffectItem item = factory.RandomPowerUp(center);
        // Precondition: EffectItem is not out of bounds
        IsWithinBounds(item);
        // Postcondition: EffectItem moves with constant speed
        for (int i = 0; i < 10; i++) {
            item.Move();
        }
        Vec2F deltaPosA = center - item.GetPosition();
        for (int i = 0; i < 10; i++) {
            item.Move();
        }
        Vec2F deltaPosB = center - item.GetPosition();
        Assert.AreEqual(
            deltaPosB / 20,
            deltaPosA / 10
        );
    }
}