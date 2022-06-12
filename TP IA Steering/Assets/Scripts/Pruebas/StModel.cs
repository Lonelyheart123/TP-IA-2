using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StModel : MonoBehaviour
{
    public float speed;
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
   public void Move(Vector3 dir)
   {
        dir.y = 0;
        _rb.velocity = dir * speed;
   }
    public void LookDir(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.02f);
    }
}
