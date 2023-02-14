using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2D;
    
    [Header("jump System")]
    [SerializeField] float speed;
    [SerializeField] float jumpTime;
    [SerializeField] float jumpForce;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;

    private bool isJumping;
    private float jumpCounter; 
    
    private float moveHorizontal;
    
    private Vector2 vecGravity;

    public Transform groundCheck;
    public LayerMask groundLayer;
    
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded()) 
        {                                                               
            rb2D.velocity = new Vector2(0, jumpForce);
            isJumping = true;
            jumpCounter = 0;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (rb2D.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            rb2D.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

    }
    
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);
    }
    
    bool isGrounded()                                                                                           
     {                                                                                                           
         return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.04f),                           
             CapsuleDirection2D.Horizontal,0, groundLayer);                                                        
     }                                                                                                           

}

   
   
   
   