using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float lookRadius = 10f;
    [SerializeField] Animator myAnimator;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float health = 40f;
    [SerializeField] int originalDirection;

    Vector2 originalScale; 
    bool chasingPlayer = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Set the player as the enemy's target.
        Transform target = FindObjectOfType<PlayerMovement>().transform;
        if (!chasingPlayer)
        {

            // Chase player if they are in range.
            if (Vector2.Distance(target.transform.position, transform.position) <= lookRadius)
            {
                myAnimator.SetBool("beginChase", true);
            }
                
        }
        else
        {
            float facePlayer = originalDirection * Mathf.Sign(target.transform.position.x - transform.position.x);
            transform.localScale = new Vector2(originalScale.x * facePlayer, originalScale.y);
            Vector2 direction = target.transform.position - transform.position;
            myRigidbody.MovePosition(myRigidbody.position + moveSpeed * Time.deltaTime * direction.normalized);
        }
    }

    public void ProvokeEnemy()
    {
        chasingPlayer = true;
        myAnimator.SetBool("beginChase", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop moving and attack.
        myAnimator.SetTrigger("attack");
        moveSpeed = 0;
    }

    public void AttackPlayer()
    {
        // This is run from the animator. Deal damage to the player if they are within attacking range.

        Transform target = FindObjectOfType<PlayerHealth>().transform;
        if(Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(20f);
        }
    }

    public void FinishAttack()
    {
        moveSpeed = 4f;
        Transform target = FindObjectOfType<PlayerHealth>().transform;
        if (Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            myAnimator.SetTrigger("attack");
            moveSpeed = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        myAnimator.SetTrigger("die");
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(this);
    }

}

