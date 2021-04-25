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

        private void Start()
        {
            InitialiseMap();
            Generate();
            DrawMap();
        }

        private void InitialiseMap()
        {
            Map = new byte[_width, _depth];

            for (int z = 0; z < _depth; z++)
                for (int x = 0; x < _width; x++)
                    Map[x, z] = (int)MazeElement.Wall;
        }

        protected virtual void Generate()
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
                        Vector3 pos = new Vector3(x, 0, z);
                        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        wall.transform.position = pos;
                    }
                }
            }
        }
    }
}
