// namespace BreakoutTests.EntityTests;

// using System;
// using Breakout;
// using Breakout.Entities;
// using Breakout.Events;
// using Breakout.IO;
// using DIKUArcade.Entities;
// using DIKUArcade.Events;
// using DIKUArcade.Graphics;
// using DIKUArcade.Input;
// using DIKUArcade.Math;
// using NUnit.Framework;


// [TestFixture]
// public class TestPlayer {
//     private GameEventBus eventBus;
//     private IBaseImage playerImage;
//     private Shuttle shuttle;
//     private DynamicShape playerShape;
//     private readonly float START_POS = 0.4f;
//     private readonly float SPEED = 0.02f;


//     [SetUp]
//     public void InitiatePlayer() {
//         playerImage = Assets.LoadImage("player.png");
//         Vec2F pos = new Vec2F(START_POS, 0.1f);
//         playerShape = new DynamicShape(
//                     pos, new Vec2F(0.15f, 0.03f)
//                 );
//         eventBus = GameBus.GetBus();

//         shuttle = Shuttle.NewShuttle(pos, playerImage);

//         eventBus.Subscribe(GameEventType.PlayerEvent, shuttle);

//     }
//     private bool IsWithinBounds(Shuttle shuttle) {
//         return shuttle.GetPosition().X <= 1.0f &&
//                 shuttle.GetPosition().X >= 0.0f;
//     }

//     [Test]
//     public void TestMoveRight() {
//         Assert.IsTrue(IsWithinBounds(shuttle));
//         for (int i = 0; i < 1; i++) {
//             eventBus.RegisterEvent(new EventBuilder()
//                 .WithType(GameEventType.PlayerEvent)
//                 .WithKey(KeyboardKey.Right)
//                 .WithAction(KeyboardAction.KeyPress)
//                 .Build());
//             eventBus.ProcessEventsSequentially();
//             shuttle.Move();
//             eventBus.RegisterEvent(new EventBuilder()
//                 .WithType(GameEventType.PlayerEvent)
//                 .WithKey(KeyboardKey.Right)
//                 .WithAction(KeyboardAction.KeyRelease)
//                 .Build());
//             eventBus.ProcessEventsSequentially();

//         }

//         // Assert.IsTrue(IsWithinBounds(shuttle));

//         float expectedXPos = Math.Min(
//             START_POS + SPEED,
//             1f - shuttle.GetExtent().X
//         );
//         // string msg = $"TestCase({1}): {expectedXPos}, {shuttle.GetPosition().X}";
//         Assert.That(FloatComparer.AreAlmostEqual(expectedXPos, shuttle.GetPosition().X));
//     }
// }