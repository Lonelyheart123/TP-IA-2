using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyPatrol<T> : States<T>
    {
        public bool _canPatrol = true;

        Seek seek;
        Transform _target;
        Enemy _enemy;
        public bool inSight;
        float _distance = 0;
        protected INode _root;
        ISteering newSteering;

        public EnemyPatrol(Enemy enemyModel, Transform target, float distance, INode root)
        {
            _target = target;
            _enemy = enemyModel;
            _distance = distance;
            _root = root;
        }

        public override void Init()
        {

        }

        public override void Execute()
        {
<<<<<<< Updated upstream

            _enemy.Move(_enemy.GetDir());

=======
            Debug.Log("Patrolling");
            //_enemy.Move(/*_enemy.GetDir()*/);
            //_enemy.GetDir();
            _enemy.stController.SetNewSteering(seek);
>>>>>>> Stashed changes
            if (_enemy.IsInSight(_target))
            {
                inSight = true;
                Debug.Log("Is in sight");
                _root.execute();
            }

        }

        public override void Exit()
        {
        }
    }
}