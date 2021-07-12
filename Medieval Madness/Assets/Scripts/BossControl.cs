using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int timePerPhase = 10;
    [SerializeField] GameObject wizardBarrier;
    [SerializeField] float health = 150f;
    int behaviour = -1;
    Player player;
    bool battleEngaged = false;
    float ascendingConstant = 0f;
    float initialHeight;
    bool recharging = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        animator.SetBool("Fly", true);
        initialHeight = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= 7 && !battleEngaged)
        {
            BeginBattle();
        }
        if (battleEngaged && !recharging)
        {
            StartCoroutine(ChangePhase());
        }
        transform.position = new Vector2(transform.position.x, Mathf.Max(initialHeight, transform.position.y + Time.deltaTime * ascendingConstant));
    }

    IEnumerator ChangePhase()
    {
        recharging = true;
        behaviour = -behaviour;
        if (behaviour == 1)
        {
            DisablePhase2();
            InitiatePhase1();
        }
        if (behaviour == -1)
        {
            DisablePhase1();
            InitiatePhase2();
        }
        yield return new WaitForSeconds(timePerPhase);
        recharging = false;
    }

    private void BeginBattle()
    {
        Instantiate(wizardBarrier, new Vector2(-60.7f, -108.83f), Quaternion.identity);
        Instantiate(wizardBarrier, new Vector2(-39.67f, -108.83f), Quaternion.identity);
        battleEngaged = true;
    }

    public void DisablePhase1()
    {
        GetComponent<BossStalk>().enabled = false;
    }

    public void InitiatePhase1()
    {
        GetComponent<BossStalk>().enabled = true;
        animator.SetBool("Fly", false);
    }

    public void DisablePhase2()
    {
        GetComponent<BossFly>().enabled = false;
        StartCoroutine(Ascend(-2));
    }

    public void InitiatePhase2()
    {
        GetComponent<BossFly>().enabled = true;
        animator.SetBool("Fly", true);
        StartCoroutine(Ascend(2));
    }

    IEnumerator Ascend(int constant)
    {
        ascendingConstant = constant;
        yield return new WaitForSeconds(2);
        ascendingConstant = 0;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetTrigger("Hurt");
            animator.SetTrigger("Die");
            transform.position = new Vector2(transform.position.x, initialHeight);
            DisablePhase1();
            DisablePhase2();
            DestroyBarriers();
            this.enabled = false;
        }
        animator.SetTrigger("Hurt");
    }

    void DestroyBarriers()
    {
        foreach(GameObject barrier in GameObject.FindGameObjectsWithTag("WizardBarrier"))
        {
            Destroy(barrier);
        }
    }

}
