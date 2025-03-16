using System;
using System.Collections.Generic;

namespace SimpleShortestPath
{
    internal class Program
    {
        static void Main()
        {
            int n = 5; 
            int inf = int.MaxValue; 
            int start = 0; 
            int[,] mat = 
            {
                { inf, 1, inf, inf, 3 },
                { inf, inf, 8, 7, 1 },
                { inf, inf, inf, 1, -5 },
                { inf, inf, 2, inf, inf },
                { inf, inf, inf, 4, inf }
            };

            int[] distances = new int[n]; 
            for (int i = 0; i < n; i++)
            {
                distances[i] = inf;
            }
            distances[start] = 0; 


            for (int k = 0; k < n - 1; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (mat[i, j] != inf && distances[i] != inf)
                        {
                            distances[j] = Math.Min(distances[j], distances[i] + mat[i, j]);
                        }
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (distances[i] == inf)
                {
                    Console.WriteLine($"Расстояние до вершины {i}: бесконечность");
                }
                else
                {
                    Console.WriteLine($"Расстояние до вершины {i}: {distances[i]}");
                }
            }
        }
    }
}
