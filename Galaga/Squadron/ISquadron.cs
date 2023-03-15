namespace Galaga.Squadron;

using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);
}
