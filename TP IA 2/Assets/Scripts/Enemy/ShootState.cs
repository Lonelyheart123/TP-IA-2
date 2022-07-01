using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState<T> : States<T>
{
    FSM<T> _fsm;
  

    Transform _target;
    Enemy _enemy;
    float time = 1;
    float _counter = 0;
    Vector3 _dir;

    public ShootState(Enemy enemy, Transform target, FSM<T> _fsm, Vector3 dir)
    {
        _target = target;
        _enemy = enemy;
        this._fsm = _fsm;
        _dir = dir;
    }

    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= time && _enemy.IsInSight(_target))
        {
            _dir = (_target.transform.position - _enemy.transform.position).normalized;
           // _enemy.Shoot(_dir);
            _counter = 0;
            //_root.execute();
        }
    }
 
    public override void Execute()
    {
        Update();
        Debug.Log("Attack");
    }
    void Shoot(Vector3 dir)
    {

    }
}
