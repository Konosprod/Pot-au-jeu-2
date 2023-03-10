using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMob : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction = new Vector2(0f, -1f);


    private Shoot[] shoots;
    private Rigidbody2D rb;
    private Collider2D col;
    private Renderer rend;

    private bool isSpawned = false;


    void Awake()
    {
        shoots = GetComponents<Shoot>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Shoot shoot in shoots)
        {
            shoot.enabled = false;
        }
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
                foreach (Shoot shoot in shoots)
                {
                    shoot.enabled = true;
                }
                col.enabled = true;
            }
        }
        else
        {
            // Shoot whenever it's possible
            foreach (Shoot shoot in shoots)
            {
                shoot.Attack();
            }

            // Destroy enemies if they go off-screen after being activated
            if (!rend.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Always move in the set direction when active
        if (isSpawned)
        {
            rb.velocity = direction.normalized * speed;
        }
    }
}
