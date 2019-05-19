using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 5f;

    private Shoot[] shoots;
    private Rigidbody2D rb;
    private Collider2D col;
    private Renderer rend;

    private bool isSpawned = false;

    private Vector3 startPos;

    private float initialMovementTimer = 1f;
    private float randFactorStart = 0f;
    public float sineMoveSpeed = 0.5f;

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
        startPos = transform.position;
        randFactorStart = Random.Range(-0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned)
        {
            // Activate enemies when they are on screen
            if (rend.IsVisibleFrom(Camera.main))
            {
                isSpawned = true;
                foreach (Shoot shoot in shoots)
                {
                    shoot.enabled = true;
                }
                col.enabled = true;

                PlayerController._instance.Scrolling(false);
            }
        }
        else
        {
            // Shoot whenever it's possible
            foreach (Shoot shoot in shoots)
            {
                shoot.Attack();
            }
        }
    }

    void FixedUpdate()
    {
        if (isSpawned)
        {
            // Movement logic
            if (initialMovementTimer > 0f)
            {
                initialMovementTimer -= Time.fixedDeltaTime;
                startPos += new Vector3(-speed * Time.fixedDeltaTime, 0f, 0f);
            }

            transform.position = startPos + new Vector3(Mathf.Sin(Time.time * sineMoveSpeed), randFactorStart - Mathf.Sin(Time.time * (0.5f / sineMoveSpeed)) * 2f, 0.0f);
        }
    }

    void OnDestroy()
    {
        PlayerController._instance.Scrolling(true);
    }
}
