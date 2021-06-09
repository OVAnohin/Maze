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
            CreateCorridor(x + 1, z);
            CreateCorridor(x - 1, z);
            CreateCorridor(x, z + 1);
            CreateCorridor(x, z - 1);
        }
    }
}
