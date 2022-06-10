using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase<T> : States<T>
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

    public EnemyChase(Enemy enemyModel, Transform target, float distance, Transform npc, INode root)
    {
        _target = target;
        _enemy = enemyModel;
        _distance = distance;
        _npc = npc;
        _root = root;
    }

    public override void Init()
    {
        Debug.Log("Awake");
    }

    public Vector3 GetDir()
    {
        //PlayerDetected = true;
        Vector3 dir = (_target.position - _npc.position).normalized;
        return dir;
    }

    void MoveToPlayer()
    {
        if (enemyController.LineOfSight() == true && enemyController.ShootRange() == false)
        {
            Vector3 dir = GetDir();
            _enemy.transform.LookAt(_target.position);
            var ySpeed = _enemy.GetComponent<Rigidbody>().velocity.y;
            _enemy.GetComponent<Rigidbody>().velocity = new Vector3(dir.x * _enemy.speed, ySpeed, dir.z * _enemy.speed);
        }
        else if (enemyController.LineOfSight() == true && enemyController.ShootRange() == true)
        {
            _root.execute();
        }
        else if (enemyController.LineOfSight() == false && enemyController.ShootRange() == false)
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
