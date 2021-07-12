using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileFiringPeriod;
    [SerializeField] float shotsTillOverHeat;
    [SerializeField] float coolDownTime;
    [SerializeField] bool shieldTurret = false;
    [SerializeField] float turretRange = 10f;
    [SerializeField] bool overHeat = false;
    [SerializeField] float health = 50f;
    [SerializeField] Animator animator;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject explosionSound;

    GameObject[] invaders;
    bool allowFire = true;
    float shotsFired = 0;

    // Update is called once per frame
    void Update()
    {

        invaders = GameObject.FindGameObjectsWithTag("Invader");

        if (!shieldTurret)
        {
            if (IsInRange(invaders))
            {
                animator.SetBool("inRange", true);
                if (!overHeat)
                {
                    Fire();
                    if (shotsFired >= shotsTillOverHeat)
                    {
                        overHeat = true;
                        shotsFired = 0;
                        StartCoroutine(TurretCooldown());
                    }
                }
            }
            else
            {
                animator.SetBool("inRange", false);
            }
        }
  
    }

    private void Fire()
    {
        animator.SetBool("inRange", true);
        if (allowFire)
        {
            allowFire = false;
            StartCoroutine(FireContinuously());
        }

    }

    IEnumerator FireContinuously()
    {
        Instantiate(projectile, new Vector2(transform.position.x - 0.2f, transform.position.y + 0.15f), Quaternion.identity);
        shotsFired++;
        yield return new WaitForSeconds(projectileFiringPeriod);
        allowFire = true;
    }

    IEnumerator TurretCooldown()
    {
        yield return new WaitForSeconds(coolDownTime);
        overHeat = false;
    }

    public bool IsInRange(GameObject[] invaders)
    {
        Vector2 currentPosition = gameObject.transform.position;

        foreach (GameObject potentialTarget in invaders)
        {
            float xDistanceToTarget = Mathf.Abs(potentialTarget.transform.position.x - currentPosition.x);
            float yDistanceToTarget = Mathf.Abs(potentialTarget.transform.position.y - currentPosition.y);
            //See if there is at least one invader in range and in the right lane
            if (xDistanceToTarget < turretRange && yDistanceToTarget < 1f)
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionSound, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvaderProjectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(projectile.GetDamage());
            Destroy(collision.gameObject);
        }
    }

}
