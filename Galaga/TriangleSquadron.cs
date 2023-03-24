namespace Galaga;
using System.Collections.Generic;
using DIKUArcade.Graphics;

/// <summary> A squadron in the shape of an triangle </summary>
public class TriangleSquadron : BaseSquadron {
    private static int[,] formation =
        new int[,] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };
    public TriangleSquadron (
        List<Image> enemyStride,
        List<Image> alternativeEnemyStride
    ) : base(formation) {
        this.CreateEnemies(enemyStride,alternativeEnemyStride);
    }
}