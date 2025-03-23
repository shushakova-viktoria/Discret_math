using System;

class Program
{
    const int inf = int.MaxValue;

    static void Floyd(int[,] graph, int vertexCount)
    {
        int[,] distShort = new int[vertexCount, vertexCount];
        int[,] node = new int[vertexCount, vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = 0; j < vertexCount; j++)
            {
                distShort[i, j] = graph[i, j];
                if (graph[i, j] != inf && i != j)
                {
                    node[i, j] = j; 
                }
                else
                {
                    node[i, j] = -1; 
                }
            }
        }

        for (int k = 0; k < vertexCount; k++)
        {
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (distShort[i, k] != inf && distShort[k, j] != inf)
                    {
                        if (distShort[i, j] > distShort[i, k] + distShort[k, j])
                        {
                            distShort[i, j] = distShort[i, k] + distShort[k, j];
                            node[i, j] = node[i, k];
                        }
                    }
                }
            }
        }

        PrintAnswer(distShort, node, vertexCount);
    }

    static void PrintAnswer(int[,] dist, int[,] node, int vertexCount)
    {
        Console.WriteLine("матрица кратчайших путей:");

        Console.Write("\t");
        for (int j = 0; j < vertexCount; j++)
        {
            Console.Write($"[{j}]\t");
        }
        Console.WriteLine();

        for (int i = 0; i < vertexCount; i++)
        {
            Console.Write($"[{i}]\t");
            for (int j = 0; j < vertexCount; j++)
            {
                if (dist[i, j] == inf)
                    Console.Write("inf\t");
                else
                    Console.Write(dist[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nпути:");
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = 0; j < vertexCount; j++)
            {
                if (i != j && dist[i, j] != inf)
                {
                    Console.Write($"путь от {i} до {j}: ");
                    PrintPath(i, j, node);
                    Console.WriteLine($" (длина: {dist[i, j]})");
                }
            }
        }
    }

    static void PrintPath(int start, int end, int[,] node)
    {
        if (node[start, end] == -1)
        {
            Console.Write("нет пути");
            return;
        }

        Console.Write(start);
        while (start != end)
        {
            start = node[start, end];
            Console.Write(" -> " + start);
        }
    }

    static void Main(string[] args)
    {
        int[,] graph = new int[,] {
            { 0, 10, 18, 8, inf, inf },
            { 10, 0, 16, 9, 21, inf },
            { inf, 16, 0, inf, inf, 15 },
            { 7, 9, inf, 0, inf, 12 },
            { inf, inf, inf, inf, 0, 23 },
            { inf, inf, 15, inf, 23, 0 }
        };
        int vertexCount = 6;
        Floyd(graph, vertexCount);
    }
}