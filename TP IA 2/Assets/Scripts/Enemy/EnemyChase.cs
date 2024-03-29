﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class EnemyChase<T> : States<T>
    {
        PlayerMove _target;
        Enemy _enemy;
        EnemyController _enemyController;
        float _distance = 1;
        public float predictionTime = 1;
        public float _radius;
        public float _range = 30;
        public float _angle;
        public float _avoidanceWeight = 1;
        public float _steeringWeight = 1;
        Transform _transform;
        public LayerMask _obsMask;
        public ISteering _currentSteering;
        public ISteering _avoidance;
        INode _root;

        public EnemyChase(Enemy enemyModel, EnemyController enemyController, PlayerMove target, float distance, INode root, float Radius, float Range, float Angle, Transform Transform, LayerMask ObsMask, ISteering CurrentSteering, ISteering Avoidance, float AvoidanceWeight, float SteeringWeight)
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
            _avoidance = Avoidance;
            _avoidanceWeight = AvoidanceWeight;
            _steeringWeight = SteeringWeight;
        }

        public override void Init()
        {
            InitializedSteering();
        }
        void InitializedSteering()
        {
            var pursuit = new Pursuit(_transform, _target.transform, _target, predictionTime);
            _avoidance = pursuit;
        }
        public Vector3 GetDir()
        {
            Vector3 dir = _avoidance.GetDir();
            return dir;
        }
        public void SetNewSteering(ISteering newSteering)
        {
            _avoidance = newSteering;
        }
        void MoveToPlayer()
        {
            bool isLineOfSight = _enemy.IsInSight(_target.transform);
            bool isInShootRange = _enemyController.ShootRange();

            if (isInShootRange)
            {
                _enemy.CanSeePlayer();
                _root.execute();
            }
            else if (isLineOfSight)
            {
                _enemy.CanSeePlayer();
                Vector3 dir = GetDir();
                _enemy.transform.LookAt(_target.transform.position);
                var ySpeed = _enemy.GetComponent<Rigidbody>().velocity.y;
                _enemy.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * _enemy.speed, ySpeed, dir.z * _enemy.speed);
            }
            else
            {
                _enemy.CantSeePlayer();
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
