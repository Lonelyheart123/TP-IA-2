using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyChase<T> : EnemyPatrol<T>
    {
        GameObject Player;
<<<<<<< Updated upstream
        //public bool _canPatrol = true;
        Transform _target;
        Enemy _enemy;
        EnemyController _enemyController;
        //public bool inSight;
=======
        public bool _canPatrol = true;

        EnemyController _enemyController;
        Transform _target;
        Enemy _enemy;
        Pursuit pursuit;
        public bool inSight;
>>>>>>> Stashed changes
        float _distance = 0;

        Transform _npc;
        //INode _root;

<<<<<<< Updated upstream
        public EnemyChase(Enemy enemyModel, EnemyController enemyController, Transform target, float distance, INode root) : base(enemyModel, target, distance, root)
=======
        public EnemyChase(Enemy enemyModel, EnemyController enemyController,Transform target, float distance, INode root) : base(enemyModel, target, distance, root)
>>>>>>> Stashed changes
        {
            _target = target;
            _enemyController = enemyController;
            _enemy = enemyModel;
            _distance = distance;
            base._root = root;
        }

        public override void Init()
        {
        }

        public Vector3 GetDir()
        {
            Vector3 dir = _enemy.GetDir().normalized;
            return dir;
        }

        void MoveToPlayer()
        {
            bool isLineOfSight = _enemyController.LineOfSight();
            bool isInShootRange = _enemyController.ShootRange();
            if (isLineOfSight && !isInShootRange)
            {
                Vector3 dir = GetDir();
                _enemy.transform.LookAt(_target.position);
                var ySpeed = _enemy.GetComponent<Rigidbody>().velocity.y;
                _enemy.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * _enemy.speed, ySpeed, dir.z * _enemy.speed);
            }
            else if (isLineOfSight && isInShootRange)
            {
                base._root.execute();
            }
            else 
            {
                base._root.execute();
            }
        }

        public override void Execute()
        {
            _enemy.stController.SetNewSteering(pursuit);
            MoveToPlayer();
            Debug.Log("Chasing");
        }

        public override void Exit()
        {
            Debug.Log("Enemy ChaseState sleep");
        }
    }
}
