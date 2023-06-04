namespace BreakoutTests.EntityTests;

using System;
using NUnit.Framework;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using Breakout.Entities;
using Breakout.Entities.EffectItems;

[TestFixture]
public class TestExtraLifeEffect {

    EffectItemHandler handler;

    [SetUp]
    public void SetUp() {
        handler = EffectItemHandler.GetInstance();
    }


}