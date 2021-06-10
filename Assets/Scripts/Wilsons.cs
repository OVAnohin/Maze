using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Wilsons : Maze
    {
        private List<MapLocation> _notUsed = new List<MapLocation>();

        protected override void CreateCorridor()
        {
            int x = Random.Range(1, Width - 1);
            int z = Random.Range(1, Depth - 1);
            Map[x, z] = (int)MazeElement.StartPosition;

            int loop = 0;
            while (GetAvailableCells() > 1)
            {
                loop++;
                RandomWalk();

                if (loop > 5000)
                    break;
            }
        }

        private int GetAvailableCells()
        {
            _notUsed.Clear();

            for (int z = 1; z < Depth - 1; z++)
                for (int x = 1; x < Width - 1; x++)
                    if (CountSquareMazeNeighbours(x, z) == (int)MazeElement.Corridor)
                        _notUsed.Add(new MapLocation(x, z));

            return _notUsed.Count;
        }

        private int CountSquareMazeNeighbours(int x, int z)
        {
            int count = 0;
            for (int d = 0; d < Directions.Count; d++)
            {
                int nextX = x + Directions[d].X;
                int nextZ = z + Directions[d].Z;
                if (Map[nextX, nextZ] == (int)MazeElement.StartPosition)
                    count++;
            }
            return count;
        }

        private void RandomWalk()
        {
            List<MapLocation> inWalk = new List<MapLocation>();
            int randomStartIndex = Random.Range(0, _notUsed.Count);

            int startX = _notUsed[randomStartIndex].X;
            int startZ = _notUsed[randomStartIndex].Z;

            bool validPath = false;

            inWalk.Add(new MapLocation(startX, startZ));

            int loop = 0;
            while (startX > 0 && startX < Width - 1 && startZ > 0 && startZ < Depth - 1 && loop < 5000 && !validPath)
            {
                loop++;

                Map[startX, startZ] = (int)MazeElement.Corridor;
                if (CountSquareMazeNeighbours(startX, startZ) > 1)
                    break;

                int random = Random.Range(0, Directions.Count);
                int nextX = startX + Directions[random].X;
                int nextZ = startZ + Directions[random].Z;
                if (CountSquareNeighbours(nextX, nextZ) < 2)
                {
                    startX = nextX;
                    startZ = nextZ;
                    inWalk.Add(new MapLocation(startX, startZ));
                }

                validPath = CountSquareMazeNeighbours(startX, startZ) == 1;
            }

            if (validPath)
            {
                Map[startX, startZ] = (int)MazeElement.Corridor;

                SetValueInToPath(inWalk, MazeElement.StartPosition);
            }
            else
            {
                SetValueInToPath(inWalk, MazeElement.Wall);
            }
        }

        private void SetValueInToPath(List<MapLocation> inWalk, MazeElement element)
        {
            foreach (var item in inWalk)
                Map[item.X, item.Z] = (int)element;

            inWalk.Clear();
        }
    }
}
