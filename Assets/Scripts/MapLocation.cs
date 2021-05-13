namespace Maze
{
    public struct MapLocation
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
