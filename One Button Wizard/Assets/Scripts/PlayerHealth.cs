using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float maxHealth = 100f;
    [SerializeField] HealthDisplay healthBar;
    [SerializeField] Animator myAnimator;

    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        currentHealth = 100f;
        Destroy(FindObjectOfType<HealthDisplay>().gameObject);
        myAnimator.SetTrigger("die");
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<PlayerMovement>());
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<SceneLoader>().LoadMainMenu();
        Destroy(this);
    }

}
