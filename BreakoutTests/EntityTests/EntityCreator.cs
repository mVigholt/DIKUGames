namespace BreakoutTests.EntityTests;

using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Entities;
using Breakout.IO;


/// <summary>
/// Static helper class that can create instances of common objects
/// needed in our tests.
/// When the entities being created are singletons,
/// the static helper methods will reset them before returning them.
/// </summary>
public static class EntityCreator {

    /// <summary>Reset and return the instance of Shuttle</summary>
    public static Shuttle CreateShuttle() {
        Vec2F playerPosition = new Vec2F(0.5f - Shuttle.STD_EXTENT.X / 2, 0.03f);
        IBaseImage image = new Image(
            Path.Combine(PathFinder.Images(), "player.png")
        );
        Shuttle shuttle = Shuttle.NewShuttle(playerPosition, image);
        Shuttle.ResetShuttle(playerPosition);
        return shuttle;
    }
}