using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public static class Extensions
    {
        private static System.Random _random = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int next = _random.Next(count + 1);
                T value = list[next];
                list[next] = list[count];
                list[count] = value;
            }
        }
    }
}
