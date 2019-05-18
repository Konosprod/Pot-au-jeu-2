using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;

    [Header("Movement")]
    public float speedHorizontal = 10f;
    public float speedVertical = 10f;
    public Rigidbody2D rb;


    private Shoot[] shoots;
    private ParallaxScrolling scrolling;

    private float inputHorizontal;
    private float inputVertical;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shoots = GetComponents<Shoot>();
        scrolling = GetComponent<ParallaxScrolling>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");


        float dist = (transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, dist)).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder + 0.5f, rightBorder - 0.5f),
          Mathf.Clamp(transform.position.y, topBorder + 0.5f, bottomBorder - 0.5f),
          transform.position.z
        );
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector2(inputHorizontal * speedHorizontal, inputVertical * speedVertical);
        rb.velocity = movement;


        // Shooting
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            foreach(Shoot shoot in shoots)
                shoot.Attack();
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // Collision with enemy
        BasicMob enemy = collision.gameObject.GetComponent<BasicMob>();
        if (enemy != null)
        {
            // Kill the enemy
            Health enemyHealth = enemy.GetComponent<Health>();
            damagePlayer = enemyHealth.alive;
            if (enemyHealth != null) enemyHealth.TakeDamage(enemyHealth.hp);
        }

        // Damage the player
        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
            if (playerHealth != null) playerHealth.TakeDamage(2f);
        }
    }


    // The player stops scrolling during the boss fights
    public void Scrolling(bool start)
    {
        if(scrolling != null)
            scrolling.enabled = start;
    }
}
