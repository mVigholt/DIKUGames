namespace Galaga;

using System.Collections.Generic;
using DIKUArcade.Graphics;

public class RowSquadron : BaseSquadron {

    public RowSquadron (
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride,
        int[,] formation
    ) : base(32, formation) {
        this.CreateEnemies(enemyStride, alternativeEnemyStride);
    }
}