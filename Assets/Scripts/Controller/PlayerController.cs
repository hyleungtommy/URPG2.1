using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private Vector2 lastMoveDirection; // Stores last movement direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>(); // Get Animator
    }

    void Update()
    {
        // Get input from WASD or Arrow Keys
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveY = Input.GetAxisRaw("Vertical");  

        // Disable diagonal movement
        if (moveX != 0) moveY = 0; 

        movement = new Vector2(moveX, moveY);

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement; // Store last movement direction
        }

        // Send values to Animator
        animator.SetFloat("MoveX", lastMoveDirection.x);
        animator.SetFloat("MoveY", lastMoveDirection.y);
        animator.SetBool("IsWalking", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        // Move the player
        rb.velocity = movement * moveSpeed;
    }
}
