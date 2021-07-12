using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    public float speed = 5f;
    [SerializeField] Transform playerPos;
    [SerializeField] float attackRange = 2f;
    Rigidbody2D bossRB;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        bossRB = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(playerPos.position.x, bossRB.position.y);
        Vector2 newPos = Vector2.MoveTowards(bossRB.position, target, speed * Time.deltaTime);
        bossRB.MovePosition(newPos);
        bossRB.transform.localScale = new Vector2(Mathf.Sign(playerPos.position.x - bossRB.position.x), 1);

        if (Vector2.Distance(playerPos.position, bossRB.position) <= attackRange)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("AttackNumber", 0);
    }


}
