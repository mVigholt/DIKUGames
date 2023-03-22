namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Galaga.Squadron;


/// <summary> A squadron in the shape of an X </summary>
public class XSquadron : BaseSquadron {

    public XSquadron(
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride,
        int[,] formation
    ) : base(/*32,*/ formation) {
        CreateEnemies(enemyStride, alternativeEnemyStride);
    }
}