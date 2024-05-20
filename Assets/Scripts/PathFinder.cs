using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviourSingleton<PathFinder>
{
    /*
     * A* Algorithm
     *
     * I've chose this Algorithm, because this one provide me with path every time,
     * i was reconsidering usage of greedy algorithm but there is an option that
     * it will provide not shortest path or don't return path at all, so i decided to use this one
     * 
     */
    
    private const int Cost = 1; // cost

    public List<BoardSlot> FindPath(BoardSlot start, BoardSlot finish)
    {
        var openSet = new List<BoardSlot>() { start };
        var closedSet = new HashSet<BoardSlot>();

        var cameFrom = new Dictionary<BoardSlot, BoardSlot>();

        var gScore = new Dictionary<BoardSlot, float>
        {
            [start] = 0
        };

        var fScore = new Dictionary<BoardSlot, float>
        {
            [start] = CostEstimate(start, finish)
        };

        while (openSet.Count > 0)
        {
            var current = GetLowestFScoreSlot(openSet, fScore);

            if (current == finish)
                return Reconstruct(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);
            
            foreach (var n in current.neighbors)
            {
                if (n == null || n.slotRole == SlotRole.Obstacle || closedSet.Contains(n))
                    continue;

                var tentativeGScore = gScore[current] + Cost;

                if (!openSet.Contains(n))
                    openSet.Add(n);
                else if (tentativeGScore >= gScore[n])
                    continue;

                cameFrom[n] = current;
                gScore[n] = tentativeGScore;
                fScore[n] = gScore[n] + CostEstimate(n, finish);
            }
        }
        
        return null;
    }
    
    private float CostEstimate(BoardSlot a, BoardSlot b)
        => Mathf.Abs(a.gridPos.x - b.gridPos.x) + Mathf.Abs(a.gridPos.y - b.gridPos.y);

    private BoardSlot GetLowestFScoreSlot(List<BoardSlot> openSet, Dictionary<BoardSlot, float> fScore)
    {
        var lowest = openSet[0];
        foreach (var s in openSet.Where(s => fScore.ContainsKey(s) && fScore[s] < fScore[lowest]))
            lowest = s;

        return lowest;
    }

    private List<BoardSlot> Reconstruct(Dictionary<BoardSlot, BoardSlot> cameFrom, BoardSlot current)
    {
        var toReturn = new List<BoardSlot>() { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            toReturn.Insert(0, current);
        }

        return toReturn;
    }
}
