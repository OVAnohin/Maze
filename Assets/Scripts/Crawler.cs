using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Crawler : Maze
    {
        protected override void CreateCorridor()
        {
            int x, z;

            x = 1;
            for (int i = 0; i < 3; i++)
            {
                z = Random.Range(1, Depth - 1);
                GenerateWay(Map, x, z, Depth, "Depth");
            }


            z = 1;
            for (int i = 0; i < 2; i++)
            {
                x = Random.Range(1, Width - 1);
                GenerateWay(Map, x, z, Width, "Width");
            }
        }

        private void GenerateWay(byte[,] map, int x, int z, int limit, string wayPoint)
        {
            bool done = false;

            while (!done)
            {
                map[x, z] = (int)MazeElement.Corridor;
                CreateWayPoint(ref x, ref z, limit, wayPoint);

                done |= (x < 1 || x >= Width - 1 || z < 1 || z >= Depth - 1);
            }
        }

        private void CreateWayPoint(ref int x, ref int z, int limit, string wayPoint)
        {
            var step = Random.Range(-1, 2);
            if (Random.Range(0, 100) < 50)
            {
                if (wayPoint == "Width")
                {
                    if ((step + x > 0) && (step + x < limit - 1))
                        x += step;
                }
                else
                {
                    x += Random.Range(0, 2);
                }
            }
            else
            {
                if (wayPoint == "Depth")
                {
                    if ((step + z > 0) && (step + z < limit - 1))
                        z += step;
                }
                else
                {
                    z += Random.Range(0, 2);
                }
            }
        }
    }
}
