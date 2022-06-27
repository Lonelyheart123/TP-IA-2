using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IVel
{
    public float speed;
    Rigidbody _rb;
    public CharacterController controller;

    public float GetVel => _rb.velocity.magnitude;

    public Vector3 GetFoward => transform.forward;
    //public Vector3 GetFoward => _rb.velocity.normalized;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
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
    public void SetWayPoints(List<Node> newPoints)
    {
        if (newPoints.Count == 0) return;
        var pos = newPoints[newPoints.Count - 1].transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
