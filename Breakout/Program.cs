namespace Breakout;

using DIKUArcade.GUI;

///<summary>
/// The main program, which is the entry point of the application
///</summary>
class Program {
    static void Main(string[] args) {
        var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        var game = new Game(windowArgs);
        game.Run();
    }
}
