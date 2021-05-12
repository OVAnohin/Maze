using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Prims : Maze
    {
        protected override void CreateCorridor()
        {
            int x = 2;
            int z = 2;

            List<MapLocation> walls = new List<MapLocation>();

            CreateWalls(x, z, walls);

            int countLoops = 0;
            while (walls.Count > 0 && countLoops < 5000)
            {
                int randomWall = Random.Range(0, walls.Count);
                x = walls[randomWall].X;
                z = walls[randomWall].Z;
                walls.RemoveAt(randomWall);

                if (CountSquareNeighbours(x, z) == 1)
                    CreateWalls(x, z, walls);

                countLoops++;
            }
        }

        private void CreateWalls(int x, int z, List<MapLocation> walls)
        {
            Map[x, z] = (int)MazeElement.Corridor;
            walls.Add(new MapLocation(x + 1, z));
            walls.Add(new MapLocation(x - 1, z));
            walls.Add(new MapLocation(x, z + 1));
            walls.Add(new MapLocation(x, z - 1));
        }

        private struct MapLocation
        {
            public int X => _x;
            public int Z => _z;

            private int _x;
            private int _z;

            public MapLocation(int x, int z)
            {
                _x = x;
                _z = z;
            }
        }
    }
}
