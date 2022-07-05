using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : MonoBehaviour
{
    public FSM<states> _fsm;
    Enemy _enemy;
    public Transform target;
    public PlayerMove playerMove;

    ISteering _steering;
    ISteering _avoidance;

    public float timePrediction;
    Vector3 dir;

    public float shootRange;
    private float dist;
    private float radius;
    private float angle;
    private float avoidanceWeight;
    private INode _root;

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
        InitializedTree();
        InitializedFSM();
    }
    void InitializedFSM()
    {
        IStates<states> patrol = new EnemyPatrol<states>(_enemy, target, dist, _root, _enemy.radius, _enemy.range, _enemy.angle, _enemy._points, _enemy.walkPointRange, _enemy._currentIndex, _enemy.transform, _enemy._sense, _enemy.obstacleMask, _enemy._currentSteering);
        IStates<states> chase = new EnemyChase<states>(_enemy, this, playerMove, dist, _root, _enemy.radius, _enemy.range, _enemy.angle, _enemy.transform, _enemy.obstacleMask, _enemy._currentSteering, _enemy._avoidance, _enemy._avoidanceWeight, _enemy._steeringWeight);
        IStates<states> attack = new EnemyAttack<states>(_enemy, this, target, dist, dir, _root);

        patrol.AddTransition(states.Chase, chase);
        patrol.AddTransition(states.Attack, attack);

        chase.AddTransition(states.Attack, attack);
        chase.AddTransition(states.Patrol, patrol);

        attack.AddTransition(states.Chase, chase);
        attack.AddTransition(states.Patrol, patrol);

        _fsm.SetInit(patrol);

        //_fsm = new FSM<states>(patrol);
    }
    void InitializedTree()
    {
        //Actions
        INode attack = new ActionNode(() => _fsm.Transition(states.Attack));
        INode chase = new ActionNode(() => _fsm.Transition(states.Chase));
        INode patrol = new ActionNode(() => _fsm.Transition(states.Patrol));
        //Questions
        INode qIsEnemyClose = new QuestionNode(ShootRange, attack, chase);
        INode qLineOfSight = new QuestionNode(LineOfSight, qIsEnemyClose, patrol);

        _root = qLineOfSight;
    }
    public bool LineOfSight()
    {
        return _enemy.canSeePlayer;
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