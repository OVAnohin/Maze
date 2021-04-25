using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Crawler : Maze
    {
        protected override void Generate()
        {
            bool done = false;
            int x = Width / 2;
            int z = Depth / 2;

            while (!done)
            {
                Map[x, z] = 0;
                if (Random.Range(0, 100) < 50)
                    x += Random.Range(-1, 2);
                else
                    z += Random.Range(-1, 2);
                done |= (x < 0 || x >= Width || z < 0 || z >= Depth);
            }
        }
    }
}
