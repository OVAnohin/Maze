using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Wilsons : Maze
    {
        private List<MapLocation> directions = new List<MapLocation>() { new MapLocation(0, 1), new MapLocation(1, 0), new MapLocation(-1, 0), new MapLocation(0, -1) };

        protected override void CreateCorridor()
        {
            int x = Random.Range(1, Width - 1);
            int z = Random.Range(1, Depth - 1);
            Map[x, z] = (int)MazeElement.Corridor;

            RandomWalk();
        }

        private void RandomWalk()
        {
            int startX = Random.Range(1, Width - 1);
            int startZ = Random.Range(1, Depth - 1);

            int loop = 0;
            while (startX > 0 && startX < Width - 1 && startZ > 0 && startZ < Depth - 1 && loop < 5000)
            {
                Map[startX, startZ] = (int)MazeElement.Corridor;

                int random = Random.Range(0, directions.Count);
                startX += directions[random].X;
                startZ += directions[random].Z;

                loop++;
            }
        }
    }
}
