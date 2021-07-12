using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackCooldown;
    Animator animator;
    public LayerMask enemyLayer;
    float timeUntilAttack = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Set different arrow keys to different attacks

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Attack(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Attack(2);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Attack(3);
        }

    }

    public void DamageEnemies(float damageAmount)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 1.5f, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {

            // Run the right method depending on what has been collided with
            if(enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damageAmount);
            }
            if(enemy.GetComponent<Boss>() != null)
            {
                enemy.GetComponent<Boss>().TakeDamage(damageAmount);
            }
            if(enemy.GetComponent<BossControl>() != null)
            {
                enemy.GetComponent<BossControl>().TakeDamage(damageAmount);
            }
        }
    }

    private void Attack(int attackNumber)
    {
        if (0 <= attackNumber && attackNumber < 4)
        {
            animator.SetInteger("AttackNumber", attackNumber);
        }
    }

    public void ResetAttack()
    {
        animator.SetInteger("AttackNumber", 0);
    }

}
