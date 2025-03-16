using System.Collections.Generic;

class Program
{

    static void Dijkstra(int[,] graph, int start)
    {
        int verticesCount = graph.GetLength(0);  
        int[] distances = new int[verticesCount];  
        bool[] shortestPathSet = new bool[verticesCount];  


        for (int i = 0; i < verticesCount; i++)
        {
            distances[i] = int.MaxValue;  
            shortestPathSet[i] = false;  
        }
        distances[start] = 0;  

        for (int count = 0; count < verticesCount - 1; count++)
        {

            int u = GetMinDistanceVertex(distances, shortestPathSet);
            shortestPathSet[u] = true; 


            for (int v = 0; v < verticesCount; v++)
            {
                if (!shortestPathSet[v] && graph[u, v] != 0 && distances[u] != int.MaxValue
                    && distances[u] + graph[u, v] < distances[v])
                {
                    distances[v] = distances[u] + graph[u, v];
                }
            }
        }

        PrintSolution(distances, verticesCount);
    }

    static int GetMinDistanceVertex(int[] distances, bool[] shortestPathSet)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < distances.Length; v++)
        {
            if (!shortestPathSet[v] && distances[v] <= min)
            {
                min = distances[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    static void PrintSolution(int[] distances, int n)
    {
        Console.WriteLine("Вершина \t Минимальное расстояние от начальной вершины");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(i + " \t\t " + (distances[i] == int.MaxValue ? "Нет пути" : distances[i].ToString()));
        }
    }

    static void Main(string[] args)
    {
        int[,] graph = new int[,]
        {
            { 0, 7, 9, 0, 0, 0 },
            { 7, 0, 10, 15, 0, 0 },
            { 9, 10, 0, 11, 0, 0 },
            { 0, 15, 11, 0, 6, 0 },
            { 0, 0, 0, 6, 0, 9 },
            { 0, 0, 0, 0, 9, 0 }
        };

        int startVertex = 0;  
        Dijkstra(graph, startVertex);
    }
}

