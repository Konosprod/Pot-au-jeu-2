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

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist * 0.5f)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

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

}
