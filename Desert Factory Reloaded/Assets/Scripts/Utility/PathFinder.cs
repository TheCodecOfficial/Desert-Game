using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    public static Dictionary<int, int, Node> G;
    public static List<Vector2Int> AStar(Vector2Int start, Vector2Int end)
    {
        Dictionary<int, int, Node> graph = new Dictionary<int, int, Node>();

        Node startNode = new Node(start.x, start.y);
        Node targetNode = new Node(end.x, end.y);

        graph.Add(start.x, start.y, startNode);
        graph.Add(end.x, end.y, targetNode);

        Heap<Node> openSet = new Heap<Node>(1000);
        openSet.Add(startNode);

        int maxHeapSize = 0;

        while (openSet.Count > 0)
        {
            maxHeapSize = Mathf.Max(maxHeapSize, openSet.Count);
            Node currentNode = openSet.Pop();
            currentNode.closed = true;

            if (currentNode == targetNode)
            {
                // Foccacia finito

                G = graph;
                Debug.Log("Graph size: " + graph.Count + ", max Heap size: " + maxHeapSize);

                return TraceShortestPath(startNode, targetNode);
            }

            foreach (Node neighbor in GetNeighbors(graph, currentNode))
            {
                if (neighbor.intraversable || neighbor.closed) continue;

                int moveCost = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (moveCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = moveCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.predecessor = currentNode;

                    if (!openSet.Contains(neighbor)) openSet.Add(neighbor);
                    else openSet.UpdateItem(neighbor);
                }
            }
        }
        Debug.LogWarning("NO PATH FOUND");
        return null;
    }

    static List<Vector2Int> TraceShortestPath(Node startNode, Node targetNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(new Vector2Int(currentNode.x, currentNode.y));
            currentNode = currentNode.predecessor;
        }

        return path;
    }
    static List<Node> GetNeighbors(Dictionary<int, int, Node> graph, Node node)
    {
        List<Node> neighbors = new List<Node>();

        int x = node.x;
        int y = node.y;

        // lazy town
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((i + j) % 2 == 0) continue;
                if (!graph.ContainsKey(x + i, y + j)) graph.Add(x + i, y + j, new Node(x + i, y + j));
                neighbors.Add(graph[x + i, y + j]);
            }
        }

        return neighbors;
    }
    static int GetDistance(Node from, Node to)
    {
        int dx = Mathf.Abs(from.x - to.x);
        int dy = Mathf.Abs(from.y - to.y);

        return 10 * Mathf.Max(dx, dy) + 4 * Mathf.Min(dx, dy);
    }
}
public class Node : IHeapItem<Node>
{
    public int x, y;
    public bool intraversable;
    public Node predecessor;
    int heapIndex;

    public int gCost, hCost;

    public bool closed;

    public Node(int _x, int _y)
    {
        x = _x;
        y = _y;

        if (!WorldData.ContainsTile(x, y)) intraversable = true;
        else
        {
            Tile tile = WorldData.GetTile(x, y);
            if (tile != null && tile.type != 0) intraversable = true;
        }
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node other)
    {
        int cmp = fCost.CompareTo(other.fCost);
        if (cmp == 0)
        {
            cmp = hCost.CompareTo(other.hCost);
        }
        return -cmp;
    }
}