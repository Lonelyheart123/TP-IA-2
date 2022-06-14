using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyChase<T> : EnemyPatrol<T>
    {
        GameObject Player;
        public bool _canPatrol = true;

        EnemyController enemyController;
        Transform _target;
        Enemy _enemy;
        public bool inSight;
        float _distance = 0;

        Transform _npc;
        INode _root;


        public EnemyChase(Enemy enemyModel, Transform target, float distance, INode root) : base(enemyModel, target, distance, root)
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

        public Vector3 GetDir()
        {
            //PlayerDetected = true;
            Vector3 dir = (_target.position - _enemy.position).normalized;
            return dir;
        }

        void MoveToPlayer()
        {
            bool isLineOfSight = enemyController.LineOfSight();
            bool isInShootRange = enemyController.ShootRange();
            if (isLineOfSight && !isInShootRange)
            {
                Vector3 dir = GetDir();
                _enemy.transform.LookAt(_target.position);
                var ySpeed = _enemy.GetComponent<Rigidbody>().velocity.y;
                _enemy.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * _enemy.speed, ySpeed, dir.z * _enemy.speed);
            }
            else if (isLineOfSight && isInShootRange)
            {
                _root.execute();
            }
            else 
            {
                _root.execute();
            }
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
}
