using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StController : MonoBehaviour
{
    Roulette _roulette;
    Dictionary<ActionNode, int> _rouletteNodes = new Dictionary<ActionNode, int>();

    IVel _targetvel;
    public Transform target;
    ISteering _steering;
    StModel _model;
    public float predictionTime;

    private void Start()
    {
        //CreateRoulette();
    }
    void InitializaedSteering()
    {
        var seek = new Seek(transform, target.transform);
        var flee = new Flee(transform, target.transform);
        var pursuit = new Pursuit(transform, target.transform, _targetvel, predictionTime);
        _steering = seek;
    }
    private void Awake()
    {
        InitializaedSteering();
        _model = GetComponent<StModel>();
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
    //public void CreateRoulette()
    //{
    //    Debug.Log("Ruleta Enemy Creada");
    //    _roulette = new Roulette();

    //    ActionNode healing = new ActionNode();
    //    ActionNode shoot = new ActionNode();
    //    ActionNode idle = new ActionNode();

    //    _rouletteNodes.Add(healing, 33);
    //    _rouletteNodes.Add(idle, 33);
    //    _rouletteNodes.Add(shoot, 33);

    //    ActionNode rouletteAction = new ActionNode(RouletteAction);

    //}
    public void RouletteAction()
    {
        ActionNode nodeRoulette = _roulette.Run(_rouletteNodes);
        nodeRoulette.execute();
    }
    private void OnDrawGizmos()
    {
        if (_steering == null) return;
        Gizmos.color = Color.red;
        var dir = _steering.GetDir();
        Gizmos.DrawRay(transform.position, dir * 2);
    }
}