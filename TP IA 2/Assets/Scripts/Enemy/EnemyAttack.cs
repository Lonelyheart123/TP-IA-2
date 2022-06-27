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
        INode _root;

        public EnemyAttack(Enemy enemyModel, EnemyController enemyController,Transform target, float distance, Vector3 dir, INode root)
        {
            _target = target;
            _enemy = enemyModel;
            _enemyController = enemyController;
            _distance = distance;
            _dir = dir;
            _root = root;
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
            else
            {
                _root.execute();
            }
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