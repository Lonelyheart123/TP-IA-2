using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IVel
{
    public Action OnCollision = delegate { };
    public float GetVel => _rb.velocity.magnitude;
    public Vector3 GetFoward => transform.forward;

    Transform _target;
    Transform _entity;
    Transform _transform;
    Rigidbody _rb;
    Seek seek;
    public EnemyBullet _enemyBullet;
    public EnemyController enemyController;
    public StController stController;

    public float range = 30;
    public float angle = 90;
    public int speed;

    public List<Transform> _points;
    public float walkPointRange = 1;
    int _currentIndex = 0;
    [SerializeField] int _sense;

    public LayerMask maskEnemies;
    internal Vector3 position;

    int _lastFrameLOS;
    bool _cacheLOS;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        stController = GetComponent<StController>();
    }

    //CHECK-ENEMIES
    public GameObject[] CheckEnemies()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, range, maskEnemies);
        GameObject[] objs = new GameObject[colls.Length];
        for (int i = 0; i < colls.Length; i++)
        {
            objs[i] = colls[i].gameObject;
        }
        return objs;
    }

    //MOVE
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        _rb.velocity = dir * speed;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
    }
    //LOOK-DIR
    public void LookDir(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.2f);
    }
    //GET-DIR
    //public Vector3 GetDir()
    //{
    //    Vector3 point = _points[_currentIndex].position;
    //    point.y = _transform.position.y;
    //    Vector3 dir = point - _transform.position;
    //    float distance = dir.magnitude;
    //    if (distance < walkPointRange)
    //    {
    //        _currentIndex += _sense;
    //        if (_currentIndex >= _points.Count || _currentIndex < 0)
    //        {
    //            _sense *= -1;
    //            _currentIndex += _sense * 1;
    //        }
    //    }
    //    return dir.normalized;
    //}

    public Vector3 GetDir()
    {
        Vector3 dir = stController._currentSteering.GetDir();
        return dir;
    }

    //IN-SIGHT
    public bool IsInSight(Transform target)
    {
        Vector3 diff = (transform.position - target.position);
        float distance = diff.magnitude;
        if (distance > range) return false;

        float angleToTarget = Vector3.Angle(transform.position, diff);
        if (angleToTarget > angle / 2) return false;

        Vector3 dirToTarget = diff.normalized;
        if (Physics.Raycast(transform.position, dirToTarget, distance, maskEnemies)) return false;

        return true;
    }
    //ATTACK
    public void Attack(Vector3 dir)
    {
        EnemyBullet bullet = Instantiate<EnemyBullet>(_enemyBullet);
        bullet.transform.position = transform.position;
        bullet.SetDir = dir;
    }
    //ON-COLLISION-ENTER
    private void OnCollisionEnter(Collision collision)
    {
        var entity = collision.gameObject.GetComponent<IEntity>();
        if (entity != null)
        {
            OnCollision();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Enemy dead");
            Destroy(this.gameObject);
        }
    }
    //GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
    }
}