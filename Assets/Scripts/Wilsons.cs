using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Wilsons : Maze
    {
        private List<MapLocation> _directions = new List<MapLocation>() { new MapLocation(0, 1), new MapLocation(1, 0), new MapLocation(-1, 0), new MapLocation(0, -1) };
        private List<MapLocation> _notUsed = new List<MapLocation>();

        protected override void CreateCorridor()
        {
            int x = Random.Range(1, Width - 1);
            int z = Random.Range(1, Depth - 1);
            Map[x, z] = (int)MazeElement.StartPosition;

            while (GetAvailableCells() > 1)
                RandomWalk();
        }

        private int GetAvailableCells()
        {
            _notUsed.Clear();

            for (int z = 1; z < Depth - 1; z++)
            {
                for (int x = 1; x < Width - 1; x++)
                {
                    if (CountSquareMazeNeighbours(x, z) == (int)MazeElement.Corridor)
                        _notUsed.Add(new MapLocation(x, z));
                }
            }

            return _notUsed.Count;
        }

        private int CountSquareMazeNeighbours(int x, int z)
        {
            int count = 0;
            for (int d = 0; d < _directions.Count; d++)
            {
                int nextX = x + _directions[d].X;
                int nextZ = z + _directions[d].Z;
                if (Map[nextX, nextZ] == (int)MazeElement.StartPosition)
                    count++;
            }
            return count;
        }

        private void RandomWalk()
        {
            List<MapLocation> inWalk = new List<MapLocation>();
            //int startX = Random.Range(1, Width - 1);
            //int startZ = Random.Range(1, Depth - 1);
            int randomStartIndex = Random.Range(0, _notUsed.Count);

            int startX = _notUsed[randomStartIndex].X;
            int startZ = _notUsed[randomStartIndex].Z;

            bool validPath = false;

            inWalk.Add(new MapLocation(startX, startZ));

            int loop = 0;
            while (startX > 0 && startX < Width - 1 && startZ > 0 && startZ < Depth - 1 && loop < 5000 && !validPath)
            {
                Map[startX, startZ] = (int)MazeElement.Corridor;

                int random = Random.Range(0, _directions.Count);
                int nextX = startX + _directions[random].X;
                int nextZ = startZ + _directions[random].Z;
                if (CountSquareNeighbours(nextX, nextZ) < 2)
                {
                    startX = nextX;
                    startZ = nextZ;
                    inWalk.Add(new MapLocation(startX, startZ));
                }
                validPath = CountSquareMazeNeighbours(startX, startZ) == 1;

                loop++;
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
