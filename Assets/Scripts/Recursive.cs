using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Recursive : Maze
    {
        protected override void CreateCorridor()
        {
            int x = Random.Range(1, Width - 1);
            int z = Random.Range(1, Depth - 1);
            CreateCorridor(x, z);
        }

        private void CreateCorridor(int x, int z)
        {
            if (CountSquareNeighbours(x, z) >= 2)
                return;

            Map[x, z] = (int)MazeElement.Corridor;

            Directions.Shuffle();
            CreateCorridor(x + Directions[0].X, z + Directions[0].Z);
            CreateCorridor(x + Directions[1].X, z + Directions[1].Z);
            CreateCorridor(x + Directions[2].X, z + Directions[2].Z);
            CreateCorridor(x + Directions[3].X, z + Directions[3].Z);
        }
    }
}
