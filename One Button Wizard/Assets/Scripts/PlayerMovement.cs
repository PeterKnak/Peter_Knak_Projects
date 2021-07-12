using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] Vector2 originalScale = new Vector2(3f, 3f);
    [SerializeField] Animator myAnimator;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Character faces direction they're moving in.
        if(movement.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(movement.x) * originalScale.x, originalScale.y);
        }
 

        // Set animation state based on movement.
        if(movement.x == 0 && movement.y == 0)
        {
            myAnimator.SetBool("isMoving", false);
        }
        else
        {
            myAnimator.SetBool("isMoving", true); ;
        }
    }

    private void FixedUpdate()
    {
        // Move player in the direction pressed.
        myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }



}
