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
        // Take damage and update health bar.
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
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
        // Set health to 100 so the Die() function is only run once.
        currentHealth = 100f;

        // Delete player scripts, colliders, healthbar and play death animation. 
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
