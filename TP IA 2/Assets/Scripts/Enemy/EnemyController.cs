using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : MonoBehaviour
{
    public FSM<states> _fsm;
    Enemy _enemy;
    public Transform target;
    public StController stController;

    ISteering _steering;
    ISteering _avoidance;

    public float timePrediction;
    Vector3 dir;

    public float shootRange;
    private float dist;
    private float radius;
    private float angle;
    private float avoidanceWeight;
    INode _root;

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
        IStates<states> patrol = new EnemyPatrol<states>(_enemy, target, dist, _root);
        IStates<states> chase = new EnemyChase<states>(_enemy, this, target, dist, _root);
        IStates<states> attack = new EnemyAttack<states>(_enemy, this, target, dist, dir, _root);

        patrol.AddTransition(states.Chase, chase);
        chase.AddTransition(states.Patrol, patrol);

        chase.AddTransition(states.Attack, attack);
        attack.AddTransition(states.Chase, chase);

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
        INode qLineOfSight = new QuestionNode(LineOfSight, chase, patrol);

        _root = qLineOfSight;
    }
    public bool LineOfSight()
    {
        bool isInSight = _enemy.IsInSight(target) ? true : false;
        Debug.Log("Line of Sight:" + isInSight);
        return isInSight;
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