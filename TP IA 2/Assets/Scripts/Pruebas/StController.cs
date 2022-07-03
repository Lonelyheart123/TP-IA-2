using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StController : MonoBehaviour
{
    public PlayerMove target;
    Entity _model;
    public float predictionTime;
    public float radius;
    public float range = 30;
    public float angle;
    public float avoidanceWeight = 1;
    public float steeringWeight = 1;
    public LayerMask obsMask;
    public ISteering _currentSteering;
    ISteering _avoidance;

    private void Awake()
    {
        _model = GetComponent<Entity>();
        InitializedSteering();
    }

    void InitializedSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(transform, target.transform, target, predictionTime);
        var evade = new Evade(transform, target.transform, target, predictionTime);
        var avoidance = new ObstacleAvoidance(transform, obsMask, radius, angle);
        _currentSteering = seek;
        _avoidance = avoidance;//sigue y esquiva obstaculos
    }
    public void SetNewSteering(ISteering newSteering)
    {
        _currentSteering = newSteering;
    }

    private void Update()
    {
        var dir = (_avoidance.GetDir() * avoidanceWeight + _currentSteering.GetDir() * steeringWeight).normalized;
        _model.LookDir(dir);
        _model.Move(transform.forward);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_currentSteering != null)
        {
            var dir = _currentSteering.GetDir();
            Gizmos.DrawRay(transform.position, dir * 2);
        }
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}