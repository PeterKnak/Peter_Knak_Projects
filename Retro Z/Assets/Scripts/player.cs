using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float speed = 5f;
    [SerializeField] int health = 200;
    [SerializeField] GameObject blood;
    [SerializeField] float durationOfBloodSpurt = 1f;
    [SerializeField] float gameOverWait = 2f;

    [Header("Projectile")]
    [SerializeField] GameObject playerBullet;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.6f;
    [SerializeField] AudioClip[] playerSounds;
    [SerializeField] [Range(0,1)] float playerSoundVolume = 0.75f;

    Coroutine firingCoroutine;

    Animator animator;
    float xMin;
    float xMax;
    bool allowFire = true;
    AudioSource myAudiosource;

    void Start()
    {
        myAudiosource = GetComponent<AudioSource>();
        SetUpMoveBoundaries();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Fire();

    }

    private void Move()
    {
        var deltaX = Time.deltaTime * speed * Input.GetAxis("Horizontal");
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void Fire()
    {
        if ((Input.GetButton("Fire1"))&&(allowFire))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else
        {
            animator.SetBool("IsFire", false);
        }
    }

    IEnumerator FireContinuously()
    {
        allowFire = false;
        AudioSource.PlayClipAtPoint(playerSounds[0], this.transform.position, playerSoundVolume);
        animator.SetBool("IsFire", true);
        Vector3 bulletSpawn = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject bullet = Instantiate(playerBullet, bulletSpawn, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        yield return new WaitForSeconds(projectileFiringPeriod);
        allowFire = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        FindObjectOfType<HealthDisplay>().UpdateHealth(health);
        Destroy(other);
        AudioSource.PlayClipAtPoint(playerSounds[1], this.transform.position, playerSoundVolume);
        GameObject bloodSpurt = Instantiate(blood, transform.position, new Quaternion(0, 270, 90, 0));
        Destroy(bloodSpurt, durationOfBloodSpurt);
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        StartCoroutine(LoadGameOver());
        transform.position = new Vector2(transform.position.x, -100f);
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(gameOverWait);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.35f, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.6f, 0, 0)).x;
    }
}
