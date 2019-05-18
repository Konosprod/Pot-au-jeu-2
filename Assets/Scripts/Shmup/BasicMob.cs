using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMob : MonoBehaviour
{
    public float speed = 5f;

    private Shoot shoot;
    private Rigidbody2D rb;
    private Collider2D col;
    private Renderer rend;

    private bool isSpawned = false;


    void Awake()
    {
        shoot = GetComponent<Shoot>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shoot.enabled = false;
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            // Activate enemies when they are on screen
            if(rend.IsVisibleFrom(Camera.main))
            {
                isSpawned = true;
                shoot.enabled = true;
                col.enabled = true;
            }
        }
        else
        {
            // Shoot whenever it's possible
            shoot.Attack();

            // Destroy enemies if they go off-screen after being activated
            if(!rend.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Always move downward when active
        if (isSpawned)
        {
            rb.velocity = new Vector2(0f, -1f) * speed;
        }
    }
}
