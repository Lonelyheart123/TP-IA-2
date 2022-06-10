using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyPatrol<T> : States<T>
    {
        public bool _canPatrol = true;

        Transform _target;
        Enemy _enemy;
        public bool inSight;
        float _distance = 0;
        protected INode _root;

        public EnemyPatrol(Enemy enemyModel, Transform target, float distance, INode root)
        {
            _target = target;
            _enemy = enemyModel;
            _distance = distance;
            _root = root;
        }

        public override void Init()
        {
            Debug.Log("Awake");
        }

        public override void Execute()
        {
            Debug.Log("Patrolling");

            _enemy.Move(_enemy.GetDir());

            if (_enemy.IsInSight(_target))
            {
                inSight = true;
                Debug.Log("Is in sight");
                _root.execute();
            }

        }

        public override void Exit()
        {
            Debug.Log("Enemy PatrolState sleep");
        }
    }
}