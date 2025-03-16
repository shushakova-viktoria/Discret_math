/* выдем количество компонентов связанности*/
using System;

class Graph
{
    public int vertices;
    public int[,] adjMatrix;

    public Graph(int vertices)
    {
        this.vertices = vertices;
        adjMatrix = new int[vertices, vertices];
    }

    public void AddEdge(int u, int v)
    {
    
        if (u >= 1 && u <= vertices && v >= 1 && v <= vertices)
        {
            adjMatrix[u - 1, v - 1] = 1; 
            adjMatrix[v - 1, u - 1] = 1; 
        }
        else
        {
            Console.WriteLine("Ошибка: индексы вершин должны быть в диапазоне от 1 до " + vertices);
        }
    }

    public void PrintAdjacencyMatrix()
    {
        Console.WriteLine("Матрица смежности:");
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                Console.Write(adjMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PrintIncidenceMatrix()
    {
        int edgeCount = 0;

        for (int i = 0; i < vertices; i++)
        {
            for (int j = i + 1; j < vertices; j++)
            {
                if (adjMatrix[i, j] == 1) edgeCount++;
            }
        }
        int[,] incidenceMatrix = new int[vertices, edgeCount];
        int edgeIndex = 0;

        for (int i = 0; i < vertices; i++)
        {
            for (int j = i + 1; j < vertices; j++)
            {
                if (adjMatrix[i, j] == 1)
                {
                    incidenceMatrix[i, edgeIndex] = 1;
                    incidenceMatrix[j, edgeIndex] = 1;
                    edgeIndex++;
                }
            }
        }
        Console.WriteLine("Матрица инцидентности:");
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < edgeCount; j++)
            {
                Console.Write(incidenceMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public int CountConnectedComponents()
    {
        bool[] visited = new bool[vertices];
        int componentCount = 0;
        for (int i = 0; i < vertices; i++)
        {
            if (!visited[i])
            {
                componentCount++;
                MarkComponent(i, visited);
            }
        }
        return componentCount;
    }

    private void MarkComponent(int vertex, bool[] visited)
    {
        visited[vertex] = true;
        for (int i = 0; i < vertices; i++)
        {
            if (adjMatrix[vertex, i] == 1 && !visited[i])
            {
                MarkComponent(i, visited);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите количество вершин: ");
        int n = int.Parse(Console.ReadLine());
        Graph graph = new Graph(n);
        Console.Write("Введите количество рёбер: ");
        int m = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите рёбра (u v):");
        for (int i = 0; i < m; i++)
        {
            string[] edge = Console.ReadLine().Split();
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            graph.AddEdge(u, v);
        }
        graph.PrintAdjacencyMatrix();
        graph.PrintIncidenceMatrix();
        int components = graph.CountConnectedComponents();
        Console.WriteLine($"Количество компонент связности: {components}");
    }
}



