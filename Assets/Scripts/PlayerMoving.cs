﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{

    public Rigidbody2D rigidbody2;
    public Animator animator;
    public PlayerComBat comBat;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    public float runspeed = 40f;
    public float jumpForce = 400f;
    

    Vector2 m_velocity = Vector2.zero;
    float horizontalmove = 0f;
    bool Grounded = true, faceright = true;

    private void Start()
    {
        rigidbody2 = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool("Grounded", Grounded);
        animator.SetFloat("Speed",Mathf.Abs(horizontalmove));

        
        if (Input.GetButtonDown("Jump"))//jump button
        {
            if(Grounded)
            {
                Grounded = false;
                rigidbody2.AddForce(new Vector2(0f, jumpForce));
                comBat.GetComponent<PlayerComBat>().SkipSkill();
            }
            
        }
        
    }
    private void FixedUpdate()
    {
       
        horizontalmove = Input.GetAxisRaw("Horizontal");
        horizontalmove *= runspeed * Time.fixedDeltaTime * 10f;
        if(horizontalmove != 0)
        {
            comBat.GetComponent<PlayerComBat>().SkipSkill();
        }
        Vector2 targetVelocity = new Vector2(horizontalmove, rigidbody2.velocity.y);
        rigidbody2.velocity = Vector2.SmoothDamp(rigidbody2.velocity, targetVelocity, ref m_velocity, m_MovementSmoothing);
        
        if (horizontalmove > 0 && !faceright)
        {

            Flip();
        }
        // ben phai
        else if (horizontalmove < 0 && faceright)
        {

            Flip();
        }

    }

    private void Flip()
    {
        faceright = !faceright;
        transform.Rotate(0f, 180f, 0f);
    }
    public  void swapgroud(bool ground)
    {
        Grounded = ground;
    }
    public bool isGrounded()
    {
        return Grounded;
    }
}
