using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speedHorizontal = 10f;
    public float speedVertical = 10f;
    public Rigidbody2D rb;

    [Header("Shooting")]
    public Shoot shoot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


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
        // Movement
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector2(inputHorizontal * speedHorizontal, inputVertical * speedVertical);
        rb.velocity = movement;


        // Shooting
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
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
}
