using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlateformerController : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public UiManager ui;

    public float runSpeed = 40f;
    public float climbSpeed = 20f;
    public bool climb = false;

    public float invincibilityTime = 1.5f;
    float hitTimer;

    public float blinkingTime = .2f;
    float blinkTimer;

    float life = 6f;
    public int leaves = 0;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager._instance.PlayMusic(SoundType.Platformer);
        hitTimer = invincibilityTime;
        blinkTimer = blinkingTime;

        ui.SetHP(life);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        verticalMove = Input.GetAxis("Vertical") * climbSpeed;


        /*
        if(Input.GetKeyDown(KeyCode.C))
        {
            ui.SwitchToUpgradeUI();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            ui.SwitchToShmupUI();
        }
        */

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        hitTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        if(climb)
        {
            controller.Climb(verticalMove * Time.fixedDeltaTime);
        }
        jump = false;
    }

    public void ToUpgrade()
    {
        ui.SwitchToUpgradeUI();
    }

    public void Eat()
    {
        leaves++;
        ui.UpdateLeaves(leaves);
    }

    public void Hit()
    {
        if (hitTimer >= invincibilityTime)
        {
            hitTimer = 0;
            StartCoroutine(blink());

            life -= 1f;

            ui.UpdateHP(life);

            if(life <= 0)
            {
                ui.GameOver(false);
                Destroy(this);
            }

        }
    }

    private IEnumerator blink()
    {
        float animationLength = invincibilityTime;
        while(animationLength > 0)
        {
            blinkTimer -= Time.deltaTime;
            animationLength -= Time.deltaTime;

            if(blinkTimer <= 0)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                blinkTimer = blinkingTime;
            }

            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.enabled = true;
    }
}
