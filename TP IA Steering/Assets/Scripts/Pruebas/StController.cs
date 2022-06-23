using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StController : MonoBehaviour
{
    Roulette _roulette;
    Dictionary<ActionNode, int> _rouletteNodes = new Dictionary<ActionNode, int>();

    Transform _target;
    Transform _entity;

    public StModel target;
    StModel _model;
    public float predictionTime;
    public float radius;
    public float range = 30;
    public float angle;
    public float avoidanceWeight = 1;
    public float steeringWeight = 1;
    public LayerMask obsMask;
    ISteering _steering;
    ISteering _avoidance;
    void InitializedSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(transform, target.transform, target, predictionTime);
        var evade = new Evade(transform, target.transform, target, predictionTime);
        var avoidance = new ObstacleAvoidance(transform, obsMask, radius, angle);
        _steering = pursuit;
        _avoidance = avoidance;//sigue y esquiva obstaculos
    }
    private void Awake()
    {   
        _model = GetComponent<StModel>();
        InitializedSteering();
        //CreateRoulette();
    }

    public void SetNewSteering(ISteering newSteering)
    {
        _steering = newSteering;
    }
    //void Seek(Transform entity, Transform target)
    //{
    //    _entity = entity;
    //    SetTarget(target);
    //}
    private void Update()
    {
        var dir = (_avoidance.GetDir() * avoidanceWeight + _steering.GetDir() * steeringWeight).normalized;
        _model.LookDir(dir);
        _model.Move(transform.forward);
    }
    public bool LineOfSight(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        float distance = diff.magnitude;
        if (distance > range) return false;

        //ANGLE
        float angleToTarget = Vector3.Angle(diff, transform.forward);
        if (angleToTarget > angle / 2) return false;

        //TARGETVIEW
        if (Physics.Raycast(transform.position, diff.normalized, distance, obsMask)) return false;

        return true;
    }
    //public void CreateRoulette()
    //{
    //    Debug.Log("Ruleta Enemy Creada");
    //    _roulette = new Roulette();

    //    ActionNode healing = new ActionNode(Seek);
    //    ActionNode shoot = new ActionNode(Attack);
    //    ActionNode idle = new ActionNode(FalseAttack);

    //    _rouletteNodes.Add(healing, 33);
    //    _rouletteNodes.Add(idle, 33);
    //    _rouletteNodes.Add(shoot, 33);

    //    ActionNode rouletteAction = new ActionNode(RouletteAction);

    //}
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