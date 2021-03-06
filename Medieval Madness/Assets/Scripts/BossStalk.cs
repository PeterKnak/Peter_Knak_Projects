using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStalk : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackDamage = 25f;
    [SerializeField] float attackDelay = 2f;
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
            float direction = Mathf.Sign(distanceFromPlayer.x);
            transform.localScale = new Vector2(direction, transform.localScale.y);
            moveSpeed = direction * Mathf.Abs(moveSpeed);
            if (Mathf.Abs(distanceFromPlayer.y) < 1 && Mathf.Abs(distanceFromPlayer.x) < attackRange)
            {
                AttackPlayer();
            }
            else
            {
                transform.position = new Vector2(transform.position.x + Time.deltaTime * moveSpeed, transform.position.y);
            }
        }

    }

    private void AttackPlayer()
    {
        isAttacking = true;
        animator.SetInteger("AttackNumber", 1);
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

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        animator.SetInteger("AttackNumber", 0);
    }

    private Vector2 FindDistanceFromPlayer()
    {
        if (player.GetComponent<Collider2D>().enabled == false) { return new Vector2(10, 10); }
        Vector2 distanceFromPlayer = player.gameObject.transform.position - transform.position;
        return distanceFromPlayer;
    }

}
