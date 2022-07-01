using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Entity _entity;
    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var dir = (new Vector3(h, 0, v)).normalized;
        _entity.Move(dir);
        _entity.LookDir(dir);
    }
}
