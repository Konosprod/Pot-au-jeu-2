using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1.0f;
    public float speed = 30f;

    public bool hasHit = false;

    [HideInInspector]
    public Vector2 direction;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f); // Self-destruct after 5 seconds
    }


    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }
}
