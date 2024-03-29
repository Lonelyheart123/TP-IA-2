using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderController : MonoBehaviour
{
    public CI_Model crash;
    public PlayerMove player;
    public Node start;
    public Node end;
    Astar<Node> _ast;
    public List<Node> path;
    public LayerMask mask;
    public LayerMask maskNodes;
    public float distanceMax;
    public float radius;
    public Vector3 offset;
    //public Box box;


    private void Awake()
    {
        _ast = new Astar<Node>();
    }

    private void Start()
    {
        AstarPathfinding();
    }

    public void AstarPathfinding()
    {
        var nearNodes = Physics.OverlapSphere(player.transform.position, 5, maskNodes);

        Collider nearNode = null;
        float nearDistance = 0;
        for (int i = 0; i < nearNodes.Length; i++)
        {
            Collider currObs = nearNodes[i];
            Vector3 dir = currObs.transform.position - player.position;//cumple con lo pedido de ISteering
            float currDistance = Vector3.Distance(player.position, currObs.transform.position);
            if (nearNode == null || nearDistance > currDistance)
            {
                nearNode = currObs;
                nearDistance = currDistance;
            }
        }
        end = nearNode.GetComponent<Node>();
        path = _ast.GetPath(start, IsSatisfied, GetNeighbours, GetCost, Heuristic);
        crash.SetWayPoints(path);
    }
    float Heuristic(Node curr)
    {
        float distanceMultiplier = 2;
        float h = 0;
        h += Vector3.Distance(curr.transform.position, end.transform.position) + distanceMultiplier;
        return h;
    }
    float GetCost(Node parent, Node child)
    {
        //float trapCost = 5;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (start != null)
            Gizmos.DrawSphere(start.transform.position + offset, radius);
        if (end != null)
            Gizmos.DrawSphere(end.transform.position + offset, radius);
        if (path != null)
        {
            Gizmos.color = Color.blue;
            foreach (var item in path)
            {
                if (item != start && item != end)
                    Gizmos.DrawSphere(item.transform.position + offset, radius);
            }
        }
    }
}
