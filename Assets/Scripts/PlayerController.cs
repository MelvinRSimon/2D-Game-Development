using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed;
    Rigidbody2D rb2d;
    public float Jump;
    //public bool canJump = true;
    //bool isLanded;

    private void Awake()
    {
        Debug.Log("The PlayerController is awake");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision: " + collision.gameObject.name);
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 31)
    //    {
    //        isLanded = true;
    //    }
    //    else
    //    {
    //        isLanded = false;
    //    }
    //}

    private void Update()
    {
        float horiSpeed = Input.GetAxisRaw("Horizontal");
        float veriSpeed = Input.GetAxisRaw("Vertical");
        float jump = Input.GetAxisRaw("Jump");

        PlayerHorizontal(horiSpeed);
        PlayerVertical(veriSpeed, jump);
        PlayerMovement(horiSpeed, veriSpeed, jump);
    }

    private void PlayerHorizontal(float hspeed)
    {
        animator.SetFloat("Speed", Mathf.Abs(hspeed));

        Vector3 scale = transform.localScale;
        if (hspeed < 0 /*&& isLanded == true*/)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (hspeed > 0 /*&& isLanded == true*/)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private void PlayerVertical(float vspeed, float _jump)
    {
        if (vspeed > 0 || _jump > 0 /*&& canJump == true*/)
        {
            //canJump = false;
            animator.SetBool("Jump", true);
            rb2d.AddForce(new Vector2(0f, Jump), ForceMode2D.Force);
        }
        else if (vspeed < 0)
        {
            animator.SetBool("Crouch", true);
        }
        else
        {

            animator.SetBool("Jump", false);
            animator.SetBool("Crouch", false);
        }
    }

    private void PlayerMovement(float hspeed, float vspeed, float _jump)
    {
        Vector3 position = transform.position;

        position.x += hspeed * speed * Time.deltaTime;

        transform.position = position;
    }
}
