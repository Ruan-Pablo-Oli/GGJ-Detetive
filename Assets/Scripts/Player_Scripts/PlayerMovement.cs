using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;

    public Rigidbody2D rb;

    public PlayerInput playerInput;

    public Vector2 moveInput;

    public Animator anim;

    private int facingDirection = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMoviment();
        HandleAnimation();

        Flip();
    }

    private void HandleAnimation()
    {
        anim.SetFloat("Movementx",Math.Abs(moveInput.x));
        if(Math.Abs(moveInput.y) > 0.1f)
        {
            anim.SetBool("OnVerticalMove",true);
        }
        else
        {
            anim.SetBool("OnVerticalMove",false);

        }
        anim.SetFloat("Movementy",moveInput.y);
    }



    private void HandleMoviment()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
    }


    private void Flip()
    {
        if(moveInput.x > .1f)
        {
            facingDirection = 1;            
        }else if(moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }

        transform.localScale = new Vector3(facingDirection,1,1);

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


}
