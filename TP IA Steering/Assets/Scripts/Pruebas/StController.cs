using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StController : MonoBehaviour
{
    //Roulette _roulette;
    //Dictionary<ActionNode, int> _rouletteNodes = new Dictionary<ActionNode, int>();

    public Entity target;
    Entity _model;
    public float predictionTime;
    public float radius;
    public float angle;
    public float avoidanceWeight = 1;
    public float steeringWeight = 1;
    public LayerMask obsMask;
    ISteering _steering;
    ISteering _avoidance;
    void InitializaedSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(transform, target.transform, target, predictionTime);
        var evade = new Evade(transform, target.transform, target, predictionTime);
        var avoidance = new ObstacleAvoidance(transform, obsMask, radius, angle);
        _steering = seek;
        _steering = avoidance;//sigue y esquiva obstaculos
    }
    private void Awake()
    {   
        _model = GetComponent<Entity>();
        InitializaedSteering();
    }
    public void SetNewSteering(ISteering newSteering)
    {
        _steering = newSteering;
    }
    private void Update()
    {
        var dir = (_avoidance.GetDir() * avoidanceWeight + _steering.GetDir() * steeringWeight).normalized;
        _model.LookDir(dir);
        _model.Move(transform.forward);
    }
   
    //public void RouletteAction()
    //{
    //    ActionNode nodeRoulette = _roulette.Run(_rouletteNodes);
    //    nodeRoulette.execute();
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_steering != null)
        {
            var dir = _steering.GetDir();
            Gizmos.DrawRay(transform.position, dir * 2);
        }
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}