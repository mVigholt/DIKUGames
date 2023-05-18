namespace Breakout.Entities.Effects.PowerUps;


public class SlowDownDeactivate : IEffect {

    public EffectItemType Type { get { return _type; } }

    private EffectItemType _type = EffectItemType.SlowDownDeactivate;
    private Shuttle _shuttle;
    
    public SlowDownDeactivate(Shuttle shuttle) {
        _shuttle = shuttle;
    }

    public void Activate() {
        // Todo: Don't have this design with a class called XDeactivate
        _shuttle.Shape.AsDynamicShape().Direction.X /= 0.5f;
        System.Console.WriteLine("PowerUp: Slowdowndeactivate");
    }
}