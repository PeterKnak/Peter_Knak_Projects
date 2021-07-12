using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.5f;
    [SerializeField] float maxTimeBetweenShots = 1.5f;
    [SerializeField] GameObject spit;
    [SerializeField] GameObject explosion;
    [SerializeField] float spitSpeed = 2f;
    [SerializeField] bool firingEnemy;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] int zombieKillPoints;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void ChangeDirection()
    {
        float newDirection = 2 * (0.5f - Mathf.Round(UnityEngine.Random.Range(0, 2)));
        transform.localScale = new Vector3(newDirection, transform.localScale.y, transform.localScale.z);
    }

       // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(10 * Time.time) % UnityEngine.Random.Range(10, 40) == 0)
        {
            ChangeDirection();
        }

        if (firingEnemy && transform.position.z >=0)
        {
            CountDownAndShoot();
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        Vector3 spitSpawn = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        GameObject bullet = Instantiate(spit, spitSpawn, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -spitSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        if (other.tag == "Projectile")
        {
            Destroy(other);
        }
        GameObject deathExplosion = Instantiate(explosion, transform.position, new Quaternion(0, 90, 90, 0));
        Destroy(deathExplosion, durationOfExplosion);
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(zombieKillPoints);
    }

}
