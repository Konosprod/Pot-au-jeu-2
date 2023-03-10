using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 3.0f;
    public bool alive = true;

    void OnEnable()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PlayerShmup"))
        {
            UiManager._instance.SetHP(hp);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0f)
        {
            if (gameObject.layer == LayerMask.NameToLayer("PlayerShmup"))
            {
                UiManager._instance.GameOver(true);
            }
            alive = false;
            Destroy(gameObject);
        }
        else
        {
            if (gameObject.layer == LayerMask.NameToLayer("PlayerShmup"))
            {
                UiManager._instance.UpdateHP(hp);
            }
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
        }
    }
}
