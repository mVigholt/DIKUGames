namespace BreakoutTests.EntityTests;

using NUnit.Framework;
using DIKUArcade.GUI; // Needed for OpenGL contexts

[SetUpFixture]
public class GlobalSetUp {
    [OneTimeSetUp]
    public void RunBeforeAnyTests() {
        Window.CreateOpenGLContext();
    }
}