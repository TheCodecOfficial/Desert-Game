using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathfinderDisplay : MonoBehaviour
{

    public Transform start, target;
    Vector2Int startVec, targetVec;
    List<Vector2Int> path;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePath();
        }
    }
    void UpdatePath()
    {
        startVec = new Vector2Int((int)start.position.x, (int)start.position.z);
        targetVec = new Vector2Int((int)target.position.x, (int)target.position.z);
        Stopwatch sw = new Stopwatch();
        sw.Start();
        path = PathFinder.AStar(startVec, targetVec);
        sw.Stop();
        UnityEngine.Debug.Log("Path length: " + path.Count + ", in " + sw.ElapsedMilliseconds + " ms.");
    }

    /*void OnDrawGizmos()
    {
        if (path == null) return;
        Gizmos.color = new Color(0, 162, 255);
        foreach (Vector2Int point in path)
        {
            Vector3 pos = new Vector3(point.x, 0, point.y);
            Gizmos.DrawCube(pos, Vector3.one * 0.2f);
        }

        foreach (Node node in PathFinder.G.Values)
        {
            if (node.predecessor != null)
            {
                Vector3 from = new Vector3(node.x, 0, node.y);
                Vector3 to = new Vector3(node.predecessor.x, 0, node.predecessor.y);
                Gizmos.DrawLine(from, to);
            }
        }
    }*/
}