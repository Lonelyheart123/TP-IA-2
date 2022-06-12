using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StController : MonoBehaviour
{
    public Entity target;
    ISteering _steering;
    Entity _model;
    public float predictionTime;
 
    void InitializaedSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(transform, target.transform, target, predictionTime);
        _steering = seek;
    }
    private void Awake()
    {
        InitializaedSteering();
        _model = GetComponent<Entity>();
    }
    public void SetNewSteering(ISteering newSteering)
    {
        _steering = newSteering;
    }
    private void Update()
    {
        var dir = _steering.GetDir();
        _model.Move(dir);
        _model.LookDir(dir);
    }
    private void OnDrawGizmos()
    {
        if (_steering == null) return;
        Gizmos.color = Color.red;
        var dir = _steering.GetDir();
        Gizmos.DrawRay(transform.position, dir * 2);
    }
}