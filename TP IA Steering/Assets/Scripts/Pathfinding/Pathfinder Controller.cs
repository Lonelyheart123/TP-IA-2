using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderController : MonoBehaviour
{
    public CI_Model crash;
    public StModel player;
    public Node start;
    public Node end;
    Astar<Node> _ast;

    private void Awake()
    {
        _ast = new Astar<Node>();
    }

    //public void AstarPathfinding()
    //{
    //    List<Node> path = _ast.GetPath(start, IsSatisfied, GetNeighbours, GetCost, Heuristic);
    //    crash.SetWayPoints(path);
    //    player.SetWayPoints(path);
    //}
    float Heuristic(Node curr)
    {
        float distanceMultiplier = 2;
        float h = 0;
        h += Vector3.Distance(curr.transform.position, end.transform.position) + distanceMultiplier;
        return h;
    }
    float GetCost(Node parent, Node child)
    {
        float trapCost = 5;
        float distanceMultiplier = 2;

        float cost = 0;
        cost += Vector3.Distance(parent.transform.position, child.transform.position) + distanceMultiplier;
        //if (child.hasTrap)
        //{
        //    cost += trapCost;
        //}
        return cost;
    }
    List<Node> GetNeighbours(Node curr)
    {
        return curr.neighbors;
    }
    bool IsSatisfied(Node curr)
    {
        return curr == end;
    }
}
