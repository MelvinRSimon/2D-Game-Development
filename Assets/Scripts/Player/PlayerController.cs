using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public float speed;
    Rigidbody2D rb2d;
    public float jumpForce;
    public GroundCheck groundCheck;

    private void Awake()
    {
        Debug.Log("The PlayerController is awake");
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horiSpeed = Input.GetAxisRaw("Horizontal");
        float veriSpeed = Input.GetAxisRaw("Vertical");

        Jump(veriSpeed);
        Crouch(veriSpeed);
        HorizontalAnimation(horiSpeed);
    }

    private void Jump(float vspeed)
    {
        if (groundCheck.isGrounded == true && (vspeed > 0 || Input.GetKeyDown(KeyCode.Space)))
        {
            animator.SetBool("Crouch", false);
            animator.SetTrigger("Jump");

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Crouch", false);
        }
    }

    private void Crouch(float vspeed)
    {
        if (vspeed < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Crouch", true);
        }
    }

    private void HorizontalAnimation(float hspeed)
    {
        animator.SetFloat("Speed", Mathf.Abs(hspeed));

        Vector3 scale = transform.localScale;
        if (hspeed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (hspeed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        Vector3 position = transform.position;
        position.x += hspeed * speed * Time.deltaTime;
        transform.position = position;
    }
}
