using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int dmg;
    public float speed;
    public float timeLife;
    float _counterLife;
    Vector3 _dir;
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public Vector3 SetDir
    {
        set
        {
            _dir = value;
        }
    }
    public void Shoot(Vector3 dir)
    {
        _rb.AddForce(dir * speed, ForceMode.Impulse);
    }

    public void Update()
    {
        _counterLife += Time.deltaTime;
        if (_counterLife >= timeLife)
        {
            _counterLife = 0;
            Destroy(this.gameObject);
        }
        _rb.velocity = _dir * speed;
    }
    //void ExecuteTimeLife()
    //{
    //    _counterLife += Time.deltaTime;
    //    if (_counterLife >= spawnTime && timeLife)
    //    {
    //        _counterLife = 0;
    //        Destroy(this.gameObject);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        IDamage damageable = collision.gameObject.GetComponent<IDamage>();
        if (damageable != null)
        {
            damageable.GetDamage(dmg);
        }
        Destroy(this.gameObject);
    }
}
