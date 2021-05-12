using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private int _width = 30;
        [SerializeField] private int _depth = 30;

        public int Width => _width;
        public int Depth => _depth;

        protected byte[,] Map;

        private int _scale = 6;

        private void Start()
        {
            InitializeMap();
            CreateCorridor();
            DrawMap();
        }

        private void InitializeMap()
        {
            Map = new byte[_width, _depth];

            for (int z = 0; z < _depth; z++)
                for (int x = 0; x < _width; x++)
                    Map[x, z] = (int)MazeElement.Wall;
        }

        protected virtual void CreateCorridor()
        {
            for (int z = 0; z < _depth; z++)
                for (int x = 0; x < _width; x++)
                    if (Random.Range(0, 100) < 50)
                        Map[x, z] = (int)MazeElement.Corridor;
        }

        private void DrawMap()
        {
            for (int z = 0; z < _depth; z++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (Map[x, z] == (int)MazeElement.Wall)
                    {
                        Vector3 pos = new Vector3(x * _scale, 0, z * _scale);
                        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        wall.transform.localScale = new Vector3(_scale, _scale, _scale);
                        wall.transform.position = pos;
                    }
                }
            }
        }

        public int CountSquareNeighbours(int x, int z)
        {
            int count = 0;
            if (x <= 0 || x >= Width - 1 || z <= 0 || z >= Depth - 1) return 5;
            if (Map[x - 1, z] == (int)MazeElement.Corridor) count++;
            if (Map[x + 1, z] == (int)MazeElement.Corridor) count++;
            if (Map[x, z - 1] == (int)MazeElement.Corridor) count++;
            if (Map[x, z + 1] == (int)MazeElement.Corridor) count++;

            return count;
        }

        public int CountDiagonalNeighbours(int x, int z)
        {
            int count = 0;
            if (x <= 0 || x >= Width - 1 || z <= 0 || z >= Depth - 1) return 5;
            if (Map[x - 1, z - 1] == (int)MazeElement.Corridor) count++;
            if (Map[x - 1, z + 1] == (int)MazeElement.Corridor) count++;
            if (Map[x + 1, z + 1] == (int)MazeElement.Corridor) count++;
            if (Map[x + 1, z - 1] == (int)MazeElement.Corridor) count++;

            return count;
        }

        public int CountAllNeighbours(int x, int z)
        {
            return CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
        }
    }
}
