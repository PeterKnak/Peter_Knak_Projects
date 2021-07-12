using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackDamage = 25f;
    [SerializeField] float attackDelay = 2f;
    [SerializeField] float health = 30f;
    [SerializeField] Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    Rigidbody2D myRigidbody2D;
    Player player;
    bool isAttacking = false;
    bool recovering = false;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !recovering)
        {
            Vector2 distanceFromPlayer = FindDistanceFromPlayer();
            if (Mathf.Abs(distanceFromPlayer.y) < 1 && Mathf.Abs(distanceFromPlayer.x) < attackRange)
            {
                AttackPlayer(distanceFromPlayer.x);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + Time.deltaTime * moveSpeed, transform.position.y);
            }
        }
        
    }

    private void AttackPlayer(float xDistance)
    {
        isAttacking = true;
        float direction = Mathf.Sign(xDistance);
        transform.localScale = new Vector2(-1* direction, transform.localScale.y);
        moveSpeed = -1 * direction * Mathf.Abs(moveSpeed);
        animator.SetInteger("AttackNumber", Random.Range(1, 3));
        StartCoroutine(ResetAttack());
    }

    public void DealDamage()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer.Length >= 1)
        {
            FindObjectOfType<GameSession>().UpdatePlayerSlider(attackDamage);
            player.TakeDamage(attackDamage);
        }
        
    }

    public void TakeDamage(float damageAmount)
    {
        recovering = true;
        health -= damageAmount;
        if (health <= 0)
        {
            health = 1000;
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Hurt");
            animator.SetTrigger("Die");
            this.enabled = false;
        }
        animator.SetTrigger("Hurt");
        recovering = false;
    }


    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        animator.SetInteger("AttackNumber", 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private Vector2 FindDistanceFromPlayer()
    {
        if(player.GetComponent<Collider2D>().enabled == false) { return new Vector2(10, 10); }
        Vector2 distanceFromPlayer = transform.position - player.gameObject.transform.position;
        return distanceFromPlayer;
    }

}
