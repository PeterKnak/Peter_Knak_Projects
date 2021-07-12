using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float health = 100f; 
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask targetMask;
    [SerializeField] float attackDamage = 35f;
    [SerializeField] float attackRange = 2f;
    int staggerCount = 0;
    Player player;
    Animator animator;
    bool bossAlive = true;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= 7)
        {
            animator.SetTrigger("PlayerEnter");
        }
    }

    public void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, targetMask);
        if(hitPlayer != null)
        {
            hitPlayer.GetComponent<Player>().TakeDamage(attackDamage);
            FindObjectOfType<GameSession>().UpdatePlayerSlider(attackDamage);
        }
    }

    public bool IsAlive()
    {
        return bossAlive;
    }

    public void TakeDamage(float damageAmount)
    {
        staggerCount++;
        if(staggerCount % 5 == 0)
        {
            animator.SetTrigger("MushroomHurt");
        }
        health -= damageAmount;
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("MushroomHurt");
            animator.SetTrigger("MushroomDie");
            bossAlive = false;
        }
    }
}
