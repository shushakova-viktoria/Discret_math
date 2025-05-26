
/*правую ветку алгоритма литла
возвращает длину дороги*/

using System;
using System.Collections.Generic;

class TravelingSalesman
{
    private const int inf = int.MaxValue;

    static void Main()
    {
        int[,] cityDistances = {
            {inf, 32, 10, 25},
            {23, inf, 20, 10},
            {20, 40, inf, 18},
            {17, 30, 45, inf}
        };

        List<int> optimalRoute = FindOptimalRoute(cityDistances);

        Console.WriteLine("Оптимальный маршрут: " + string.Join(" -> ", optimalRoute));
        Console.WriteLine("Общая дистанция: " + CalculateTotalDistance(cityDistances, optimalRoute));
    }

    static List<int> FindOptimalRoute(int[,] distanceMatrix)
    {
        int cityCount = distanceMatrix.GetLength(0);
        List<int> route = new List<int>();
        bool[] visitedCities = new bool[cityCount];

        route.Add(0);
        visitedCities[0] = true;

        while (route.Count < cityCount)
        {
            int currentCity = route[route.Count - 1];
            int closestCity = -1;
            int shortestDistance = inf;

            for (int nextCity = 0; nextCity < cityCount; nextCity++)
            {
                if (!visitedCities[nextCity] && distanceMatrix[currentCity, nextCity] < shortestDistance)
                {
                    shortestDistance = distanceMatrix[currentCity, nextCity];
                    closestCity = nextCity;
                }
            }

            route.Add(closestCity);
            visitedCities[closestCity] = true;
        }
        route.Add(0);
        return route;
    }

    static int CalculateTotalDistance(int[,] distanceMatrix, List<int> route)
    {
        int total = 0;
        for (int i = 0; i < route.Count - 1; i++)
        {
            total += distanceMatrix[route[i], route[i + 1]];
        }
        return total;
    }
}