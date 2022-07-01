using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;
    public float cooldownSpeed;
    public float recoilCooldown;
    private float accuracy;
    public float maxSpreadAngle;
    public float timeTillMaxSpread;
    public float fireRate;
    public float timeBullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldownSpeed += Time.deltaTime * 60f;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }

        }
        else
        {
            recoilCooldown -= Time.deltaTime;
            if (recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        Quaternion fireRotation = Quaternion.LookRotation(transform.forward);

        float currentSpeed = Mathf.Lerp(0.0f, maxSpreadAngle, accuracy / timeTillMaxSpread);
        fireRotation = Quaternion.RotateTowards(fireRotation, Random.rotation, Random.Range(0.0f, currentSpeed));
        if (Physics.Raycast(transform.position, fireRotation * Vector3.forward, out hit, Mathf.Infinity))
        {
            GameObject temBullet = Instantiate(bullet, shootPoint.transform.position, fireRotation);
            temBullet.GetComponent<MoveBullet>().hitPoint = hit.point;
            timeBullet++;
        }
        //Destruir bala despues de 3 segundos
        else if (timeBullet == 3)
        {
            DestroyImmediate(this.gameObject, true);
        }
    }
}
