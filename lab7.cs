using System;
using System.Collections.Generic;

public class WaveAlgorithm
{
    public static int ShortestPath(Dictionary<int, List<int>> graph, int start, int end)
    {
        if (start == end) return 0;

        var turn = new Queue<int>();
        var visited = new Dictionary<int, int>(); // key - вершина, а value - длина пути

        turn.Enqueue(start);
        visited[start] = 0;

        while (turn.Count > 0)
        {
            int current = turn.Dequeue();

            foreach (var neighbor in graph[current])
            {
                if (!visited.ContainsKey(neighbor))
                {
                    visited[neighbor] = visited[current] + 1;
                    turn.Enqueue(neighbor);

                    if (neighbor == end)
                    {
                        return visited[neighbor];
                    }
                }
            }
        }

        return -1; 
    }

    public static void Main()
    {
        var graph = new Dictionary<int, List<int>>()
        {
            {0, new List<int> {1, 2}},
            {1, new List<int> {0, 3}},
            {2, new List<int> {0, 4}},
            {3, new List<int> {1, 4, 5}},
            {4, new List<int> {2, 3, 5}},
            {5, new List<int> {3, 4}}
        };


        Console.WriteLine("Граф представлен следующими связями:");
        foreach (var unit in graph)
        {
            Console.Write($"Вершина {unit.Key} соединена с: ");
            foreach (var neighbor in unit.Value)
            {
                Console.Write(neighbor + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("\nДоступные вершины: 0, 1, 2, 3, 4, 5");


        Console.Write("\nВведите начальную вершину: ");
        int start = int.Parse(Console.ReadLine());

        Console.Write("Введите конечную вершину: ");
        int end = int.Parse(Console.ReadLine());



        if (!graph.ContainsKey(start) || !graph.ContainsKey(end))
        {
            Console.WriteLine("Ошибка: одна из вершин не существует в графе!");
            return;
        }

        int length = ShortestPath(graph, start, end);

        if (length == -1)
        {
            Console.WriteLine($"Пути между вершинами {start} и {end} не существует!");
        }
        else
        {
            Console.WriteLine($"Длина кратчайшего пути от {start} до {end}: {length}");
        }
    }
}