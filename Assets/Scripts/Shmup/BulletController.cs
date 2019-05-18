using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1.0f;
    public float speed = 30f;

    public bool hasHit = false;

    private Renderer rend;

    [HideInInspector]
    public Vector2 direction;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Destroy bullets if they go off-screen
        if (!rend.IsVisibleFrom(Camera.main))
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }
}
