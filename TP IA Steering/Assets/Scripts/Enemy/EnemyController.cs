using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : MonoBehaviour
{
    public FSM<states> _fsm;
    Enemy _enemy;
    public Transform target;
    
    ISteering _steering;
    ISteering _avoidance;
     
    public float timePrediction;
    Vector3 dir;
    public LayerMask obsMask;

    public float shootRange;
    private float dist;
    private float radius;
    private float angle;
    private float avoidanceWeight;
    
    private void Awake()
	{
		_enemy = GetComponent<Enemy>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    public enum states
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }
    private void Start()
    {
        _fsm = new FSM<states>();
        _enemy = GetComponent<Enemy>();
        InitializedFSM();
        InitializedISteering();
    }
    void InitializedFSM()
    {
       IStates<states> patrol = new EnemyPatrol<states>(_enemy, target, dist, _fsm, states.Chase);
       IStates<states> chase = new EnemyChase<states>(_enemy, target, dist, _fsm, states.Attack, states.Patrol, _enemy.transform);
       IStates<states> attack = new EnemyAttack<states>(_enemy, this,target, dist, _fsm, states.Patrol, dir);

       patrol.AddTransition(states.Chase, chase);
       chase.AddTransition(states.Patrol, patrol);

       chase.AddTransition(states.Attack, attack);
       attack.AddTransition(states.Chase, chase);

       _fsm.SetInit(patrol);

       //_fsm = new FSM<states>(patrol);
    }
    void InitializedISteering()
    {
        var seek = new Seek(transform, target.transform);//cazar
        var flee = new Flee(transform, target.transform);//escape
        var pursuit = new Pursuit(transform, target.transform, _enemy, timePrediction);//perseguir
        var evade = new Evade(transform, target.transform, _enemy, timePrediction);
        var avoidance = new ObstacleAvoidance(transform, obsMask, radius, angle);
        _steering = seek;
        _avoidance = avoidance;
    }
    public bool ShootRange()
    {
        bool isShootRange = (Vector3.Distance(transform.position, target.position) <= shootRange) ? true : false;
        Debug.Log("Is Shoot Range" + isShootRange);
        return isShootRange;
    }
    void Update()
    {
        _fsm.OnUpdate();
        var dir = (_avoidance.GetDir() * avoidanceWeight + _steering.GetDir()).normalized;
        //_enemy.Move(dir);
        //_enemy.LookDir(dir);
    }
    public void NewSetISteering(ISteering newSteering)
    {
        _steering = newSteering; 
    }
    //DestroyEnemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Enemy dead");
            Destroy(this.gameObject);
        }
    }
}