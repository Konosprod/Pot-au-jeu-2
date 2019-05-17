using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMob : MonoBehaviour
{
    public Shoot shoot;
    public Rigidbody2D rb;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot whenever it's possible
        shoot.Attack();
    }

    void FixedUpdate()
    {
        // Always move downward
        rb.velocity = new Vector2(0f, -1f) * speed;
    }
}
