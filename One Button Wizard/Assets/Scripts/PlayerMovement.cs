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

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(movement.x) * originalScale.x, originalScale.y);
        }
 
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
        myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }



}
