using System;
using System.Collections.Generic;
using System.Linq;

public class Kruskal
{
    public class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }
        public Edge(int source, int destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }

    public class DisjointSet
    {
        private int[] parent;
        private int[] rank;

        public DisjointSet(int size)
        {
            parent = new int[size + 1];
            rank = new int[size + 1];
            for (int i = 1; i <= size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.Write("Введите количество вершин: ");
        int vertices = int.Parse(Console.ReadLine());
        Console.Write("Введите количество рёбер: ");
        int edgeCount = int.Parse(Console.ReadLine());
        List<Edge> edges = new List<Edge>();
        Console.WriteLine("Введите рёбра в формате 'источник назначение вес':");
        for (int i = 0; i < edgeCount; i++)
        {
            string[] input = Console.ReadLine().Split();
            int source = int.Parse(input[0]);
            int destination = int.Parse(input[1]);
            int weight = int.Parse(input[2]);
            edges.Add(new Edge(source, destination, weight));
        }

        var sortedEdges = edges.OrderBy(e => e.Weight).ToList();
        DisjointSet ds = new DisjointSet(vertices);
        List<Edge> mst = new List<Edge>();
        int totalWeight = 0;


        foreach (var edge in sortedEdges)
        {
            int rootSource = ds.Find(edge.Source);
            int rootDestination = ds.Find(edge.Destination);
            if (rootSource != rootDestination)
            {
                mst.Add(edge);
                totalWeight += edge.Weight;
                ds.Union(rootSource, rootDestination);
            }
        }

        Console.WriteLine($"Суммарный вес рёбер остовного дерева: {totalWeight}");
        Console.WriteLine("Рёбра остовного дерева:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"{edge.Source} - {edge.Destination} (вес: {edge.Weight})");
        }
    }
}
