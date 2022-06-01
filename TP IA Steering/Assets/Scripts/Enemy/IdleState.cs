using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : States<T>
{
    Vector3 _dir;
    Transform _target;
    Enemy _enemy;
    private T _input;
    //float _counter = 0;
    private FSM<EnemyController.states> _fsm;

    public IdleState(Enemy enemy, Transform target, FSM <EnemyController.states> fsm, Vector3 dir)
    {
        _target = target;
        _enemy = enemy;
        _dir = dir;
    }
    public override void Init()
    {
        base.Init();
    }
    //public override void Execute()
    //{
    //    _counter += Time.deltaTime;
    //    var objs = _enemy.CheckEnemies();
    //    if (objs != null && objs.Length > 0)
    //    {
    //        for (int i = objs.Length - 1; i >= 0; i--)
    //        {
    //            _enemy.GetDir();
    //            _dir = (_target.transform.position - _enemy.transform.position).normalized;
    //           // _enemy.Attack(_dir);
    //            _counter = 0;
    //        }
    //    }
    //    base.Execute();
    //}
    public override void Exit()
    {
    }
}
