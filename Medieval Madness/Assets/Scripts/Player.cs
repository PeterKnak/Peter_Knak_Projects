using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float health = 100f;
    [SerializeField] Collider2D bodyCollider2D;
    [SerializeField] Collider2D feetCollider2D;
    [SerializeField] GameObject game;
    float horizontalMove;
    float startingGravity;
    Animator animator;
    Rigidbody2D myRigidBody;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        startingGravity = myRigidBody.gravityScale;
        game = GameObject.FindGameObjectWithTag("GameController");
        game.GetComponent<GameSession>().UpdatePlayerSlider(-100);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate()
    {
        animator.speed = 1;
        SetGravity();
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            TouchingLadder();
        }
        else
        {
            animator.SetBool("IsClimb", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        else
        {
         
        }
        CheckIfFalling();
        CheckIfMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            game.GetComponent<GameSession>().UpdatePlayerSlider(100);
            TakeDamage(100);
        }
    }

    private void TouchingLadder()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Climb(climbSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Climb(-climbSpeed);
            
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            myRigidBody.velocity = new Vector2(0, 0);
            animator.speed = 0;
        }
    }

    private void SetGravity()
    {
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = 0;
        }
        else
        {
            myRigidBody.gravityScale = startingGravity;
        }
    }

    private void Climb(float climbingSpeed)
    {
        animator.SetBool("IsClimb", true);
        Vector2 climbVelocity = new Vector2(0, climbingSpeed);
        myRigidBody.velocity = climbVelocity;
    }

    private void CheckIfMoving()
    {
        if (Mathf.Abs(horizontalMove) == 1)
        {
            Move();
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void CheckIfFalling()
    {
        if (myRigidBody.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    private void Move()
    {
        
        // Make player face left or right depending on input
        transform.localScale = new Vector2(horizontalMove, transform.localScale.y);
        if (myRigidBody.velocity.y == 0)
        {
            // Move player in x direction
            animator.SetBool("IsRunning", true);
            transform.position = new Vector2(transform.position.x + (horizontalMove * speed * Time.deltaTime), transform.position.y);
        }
        else
        {
            // Move player in x direction
            transform.position = new Vector2(transform.position.x + 0.5f * (horizontalMove * speed * Time.deltaTime), transform.position.y);
        }
    }

    private void Jump()
    {

        if(feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetTrigger("Jump");
            Vector2 jumpVelocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            myRigidBody.velocity = jumpVelocity;
        }
     }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("TakeDamage");
        if (health <= 0)
        {
            animator.SetTrigger("PlayerDied");
            GetComponent<Collider2D>().enabled = false;
            gameObject.layer = 0;
            health = 1000;
            game.GetComponent<GameSession>().ProcessPlayerDeath();
            this.enabled = false;
        }
    }

}
