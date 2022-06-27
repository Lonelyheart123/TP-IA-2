using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyChase<T> : EnemyPatrol<T>
    {
        GameObject Player;
        //public bool _canPatrol = true;
        PlayerMove _target;
        Enemy _enemy;
        EnemyController _enemyController;
        Pursuit pursuit;
        public bool inSight;
        float _distance = 0;
        //public bool inSight;

        public bool _canPatrol = true;


        Transform _npc;
        ISteering _steering;
        //INode _root;

        public EnemyChase(Enemy enemyModel, EnemyController enemyController, PlayerMove target, float distance, INode root) : base(enemyModel, target, distance, root)
        {
            _target = target;
            _enemyController = enemyController;
            _enemy = enemyModel;
            _distance = distance;
            base._root = root;
            _steering = new Pursuit(enemyModel.transform, target.transform, target, 5);//No HARDCODEEEEE
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
            _enemy.Move(_steering.GetDir());
            bool isLineOfSight = _enemyController.LineOfSight();
            bool isInShootRange = _enemyController.ShootRange();
            if (isLineOfSight && !isInShootRange)
            {
                Vector3 dir = GetDir();
                _enemy.transform.LookAt(_target.transform.position);
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
