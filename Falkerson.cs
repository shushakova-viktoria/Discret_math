using System;
using System.Collections.Generic;


//пример реализации алгоритма Форда-Фалкерсона на примере задачи про сети железных дорог.

//Имеется сеть железных дорог, соединяющую два города посредством ряда промежуточных городов,
//где каждой дуге сети присвоено значение, обозначающее его пропускную способность.
//Определить максимальную величину грузопотока в час, который можно пропустить по заданной железнодорожной сети.
//Формат входных данных
//Во входном файле записано сначала число N (1<=N<=100), определявшее количество узлов сети. Затем идет описание сети,
//где каждое соединение задается тремя числами – номерами узлов, которые она соединяет и пропускной способностью сети в час.
//Все соединения строго ориентированы.
//Формат выходных данных
//На экран вывести число, определяющее максимальную величину грузопотока в сутки. (*24).



class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        int[,] volume = new int[N, N];
        string line;
        while ((line = Console.ReadLine()) != null && line != "")
        {
            string[] parts = line.Split(' ');
            int from = int.Parse(parts[0]) - 1;
            int to = int.Parse(parts[1]) - 1;
            int cap = int.Parse(parts[2]);
            volume [from, to] = cap;
        }

        int source = 0;
        int sink = N - 1;
        int maxFlow = ComputeMaxFlow(volume, source, sink, N);

        Console.WriteLine(maxFlow * 24);
    }

    static int ComputeMaxFlow(int[,] capacity, int source, int sink, int N)
    {
        int[,] residual = new int[N, N];
        for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                residual[i, j] = capacity[i, j];

        int[] parent = new int[N];
        int maxFlow = 0;

        while (BFS(residual, source, sink, parent, N))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, residual[u, v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                residual[u, v] -= pathFlow;
                residual[v, u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }

    static bool BFS(int[,] residual, int source, int sink, int[] parent, int N)
    {
        bool[] visited = new bool[N];
        Queue<int> queue = new Queue<int>();

        queue.Enqueue(source);
        visited[source] = true;
        parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < N; v++)
            {
                if (!visited[v] && residual[u, v] > 0)
                {
                    queue.Enqueue(v);
                    parent[v] = u;
                    visited[v] = true;

                    if (v == sink)
                        return true;
                }
            }
        }

        return false;
    }
}
