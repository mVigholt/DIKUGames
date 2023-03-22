namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Galaga.Squadron;

public class TriangleSquadron : BaseSquadron {

    public TriangleSquadron (
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride,
        int[,] formation
    ) : base(/*8,*/ formation) {
        this.CreateEnemies(enemyStride,alternativeEnemyStride);
    }
}