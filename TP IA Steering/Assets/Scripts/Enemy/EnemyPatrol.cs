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
        //float _range = 0;

        FSM<T> _fsm;
        T _input;

        public EnemyPatrol(Enemy enemyModel, Transform target, float distance, FSM<T> _fsm, T input)
        {
            _target = target;
            _enemy = enemyModel;
            _distance = distance;
            _input = input;
            this._fsm = _fsm;
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

                _fsm.Transition(_input);

                Debug.Log("Is in sight");
            }

        }

        public override void Exit()
        {
            Debug.Log("Enemy PatrolState sleep");
        }
    }
}