using System;
using System.Collections.Generic;

internal class Program
{
    static bool HasMoreThanOneComponent(int[,] mat, int n)
    {
        bool[] visited = new bool[n];
        int componentsCount = 0;

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                componentsCount++;
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(i);
                visited[i] = true;

                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    for (int j = 0; j < n; j++)
                    {
                        if (mat[current, j] == 1 && !visited[j])
                        {
                            visited[j] = true;
                            queue.Enqueue(j);
                        }
                    }
                }
            }
        }
        return componentsCount > 1;
    }

    static void Main()
    {

        int[,] mat = {
            {0, 1, 1, 0, 0, 0},
            {1, 0, 0, 1, 0, 0},
            {1, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 1},
            {0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0}
        };

        int n = mat.GetLength(0);
        List<(int, int)> edgesToRemove = new List<(int, int)>();

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (mat[i, j] == 1)
                {
                    mat[i, j] = 0;
                    mat[j, i] = 0;

                    if (HasMoreThanOneComponent(mat, n))
                    {
                        edgesToRemove.Add((i, j));
                    }

                    mat[i, j] = 1;
                    mat[j, i] = 1;
                }
            }
        }

        Console.WriteLine("Ребра, удаление которых приводит к увеличению числа компонент связности:");
        foreach (var edge in edgesToRemove)
        {
            Console.WriteLine($"{edge.Item1}, {edge.Item2}");
        }
    }
}