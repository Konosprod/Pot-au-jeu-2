using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject startingPos;
    public GameObject endPos;
    public Vector2 Speed;
    SpriteRenderer sr;
    Rigidbody2D rb;

    bool left = true;
    Vector2 movement;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(left)
        {
            movement = Speed * Vector2.left;
        }
        else
        {
            movement = Speed * Vector2.right;
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x <= endPos.transform.position.x)
        {
            left = false;
            Flip();
        }
        if (transform.position.x >= startingPos.transform.position.x)
        {
            left = true;
            Flip();
        }

        rb.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collison)
    {
        GameObject other = collison.gameObject;
        if(other.CompareTag("Player"))
        {
            //If the player is above us
            if(other.transform.position.y > transform.position.y + sr.sprite.bounds.size.y)
            {
                //Play animation here and sound
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<PlayerPlateformerController>().Hit();
            }
        }
    }

    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
