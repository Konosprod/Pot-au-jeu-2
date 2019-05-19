using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : Shoot
{
    public int nbProj = 3;
    public float maxTotalAngle = 120f;
    public float maxIndividualAngle = 20f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        if (shootDelay <= 0f)
        {
            shootDelay = 1f / fireRate;

            int projPerSide = nbProj / 2;
            float individualAngle = Mathf.Min(maxTotalAngle / (2f * projPerSide), maxIndividualAngle);

            for (int i = 0; i < nbProj; i++)
            {
                float angle = (i - projPerSide) * individualAngle;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<BulletController>().direction = Quaternion.Euler(0f,0f,angle) * transform.right;
                bullet.GetComponent<BulletController>().damage = damage;
                bullet.transform.Rotate(new Vector3(0f, 0f, angle - 90f));
            }
        }
    }
}
