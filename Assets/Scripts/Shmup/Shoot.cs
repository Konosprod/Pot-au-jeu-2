using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Single forward shot
public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Shots per second
    public float fireRate = 6f;
    protected float shootDelay;
    public float initialDelay = 0.3f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        shootDelay = initialDelay;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    public virtual void Attack()
    {
        if (shootDelay <= 0f)
        {
            shootDelay = 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletController>().direction = transform.up;
        }
    }
}
