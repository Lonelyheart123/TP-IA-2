using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnemyStates
{
    public class EnemyAttack<T> : States<T>
    {
        Transform _target;
        Enemy _enemy;
        EnemyController _enemyController;
        float _distance = 0;
        float time = 1;
        float _counter = 0;
        Vector3 _dir;
        public EnemyBullet bulletprefab;

        FSM<T> _fsm;
        T _input;

        public EnemyAttack(Enemy enemyModel, EnemyController enemyController,Transform target, float distance, FSM<T> _fsm, T input, Vector3 dir)
        {
            _target = target;
            _enemy = enemyModel;
            _enemyController = enemyController;
            _distance = distance;
            this._fsm = _fsm;
            _input = input;
            _dir = dir;
        }
        public override void Init()
        {
            _enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        void Update()
        {
            _counter += Time.deltaTime;
            if (_counter >= time && _enemyController.ShootRange() == true)
            {
                _dir = (_target.transform.position - _enemy.transform.position).normalized;
                _enemy.Attack(_dir);
                _counter = 0;
            }
            //else
            //{
            //    _fsm.Transition(_input);
            //}
        }

        public override void Execute()
        {
            Update();
            Debug.Log("Attack");
        }

        public override void Exit()
        {
            Debug.Log("Enemy AttackState sleep");
        }
    }
}