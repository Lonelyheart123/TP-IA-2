using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase<T> : States<T>
{
    GameObject Player;
    public bool _canPatrol = true;

    EnemyController enemyController;
    Transform _target;
    Enemy _enemy;
    public bool inSight;
    float _distance = 0;
    //bool PlayerDetected = false;

    FSM<T> _fsm;
    T _input;
    T _input2;
    Transform _npc;

    public EnemyChase(Enemy enemyModel, Transform target, float distance, FSM<T> _fsm, T _input, T _input2, Transform npc)
    {
        _target = target;
        _enemy = enemyModel;
        _distance = distance;
        this._input = _input;
        this._input2 = _input2;
        this._fsm = _fsm;
        _npc = npc;
    }

    public override void Init()
    {
        Debug.Log("Awake");
    }

    public Vector3 GetDir()
    {
        //PlayerDetected = true;
        Vector3 dir = (_target.position - _npc.position).normalized;
        return dir;
    }

    void MoveToPlayer()
    {
        //if (enemyController.LineOfSight() == true && enemyController.ShootRange() == false)
        //{
        //    Vector3 dir = GetDir();
        //    _enemyModel.transform.LookAt(_target.position);
        //    var ySpeed = _enemyModel.GetComponent<Rigidbody>().velocity.y;
        //    _enemyModel.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * _enemyModel.speed, ySpeed, dir.z * _enemyModel.speed);
        //}
        //else if (enemyController.LineOfSight() == true && enemyController.ShootRange() == true)
        //{
            _fsm.Transition(_input);
        //}
    }
    
    public override void Execute()
    {
        MoveToPlayer();
        Debug.Log("Chasing"); 
    }

    public override void Exit()
    {
        Debug.Log("Enemy ChaseState sleep");
    }
}
