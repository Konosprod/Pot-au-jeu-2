using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 3.0f;
    private bool alive = true;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            if (gameObject.layer == LayerMask.NameToLayer("PlayerShmup"))
            {
                // GameOver
            }
            alive = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        BulletController bullet = otherCollider.gameObject.GetComponent<BulletController>();
        if (bullet != null && !bullet.hasHit && alive)
        {
            TakeDamage(bullet.damage);
            bullet.hasHit = true;
            Destroy(bullet.gameObject);
            Debug.Log("My layer : " + LayerMask.LayerToName(gameObject.layer) + ", bullet layer : " + LayerMask.LayerToName(otherCollider.gameObject.layer));
        }
    }
}
