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

    }


    void FixedUpdate()
    {
        // Movement
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector2(inputHorizontal * speedHorizontal, inputVertical * speedVertical);
        rb.velocity = movement;


        // Shooting
        if(Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            shoot.Attack();
        }
    }

}
