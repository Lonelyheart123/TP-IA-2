using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyChase<T> : States<T>
    {
        PlayerMove _target;
        Enemy _enemy;
        EnemyController _enemyController;
        float _distance = 0;
        public float predictionTime;
        public float _radius;
        public float _range = 30;
        public float _angle;
        Transform _transform;
        public LayerMask _obsMask;
        public ISteering _currentSteering;
        INode _root;

        public EnemyChase(Enemy enemyModel, EnemyController enemyController, PlayerMove target, float distance, INode root, float Radius, float Range, float Angle, Transform Transform, LayerMask ObsMask, ISteering CurrentSteering)
        {           
            _enemyController = enemyController;
            _enemy = enemyModel;
            _target = target;
            _distance = distance;
            _root = root;
            _radius = Radius;
            _range = Range;
            _angle = Angle;
            _transform = Transform;
            _obsMask = ObsMask;
            _currentSteering = CurrentSteering;
        }

        public override void Init()
        {
            InitializedSteering();
        }
        void InitializedSteering()
        {
            var seek = new Seek(_transform, _target.transform);
            _currentSteering = seek;//sigue y esquiva obstaculos
        }
        public Vector3 GetDir()
        {
            Vector3 dir = _currentSteering.GetDir();
            return dir;
        }
        public void SetNewSteering(ISteering newSteering)
        {
            _currentSteering = newSteering;
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
