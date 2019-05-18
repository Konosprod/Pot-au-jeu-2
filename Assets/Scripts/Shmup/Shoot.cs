using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Shots per second
    public float fireRate = 6f;
    private float shootDelay;

    // Start is called before the first frame update
    void Start()
    {
        shootDelay = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (shootDelay <= 0f)
        {
            shootDelay = 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletController>().direction = transform.up;
        }
    }
}
