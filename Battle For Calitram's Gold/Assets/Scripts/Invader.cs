using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float health;
    [SerializeField] Animator animator;
    [SerializeField] float damage;
    [SerializeField] float weaponRange;
    [SerializeField] bool inRange = false;
    [SerializeField] GameObject deathSound;

    Turret turret;
    GameObject[] turrets;

    private void Start()
    {
        // There is a little bit of randomness here because I don't want invaders to be bunched up
        weaponRange += Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        // By default the invader is out of range of all turrets
        float distanceToTurret = Mathf.Infinity;
        inRange = weaponRange >= distanceToTurret;

        // Find the closest turret on the invader's lane
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject turretObject = GetClosestTurret(turrets);

        // This if statement is necessary because sometimes an invader might try to access a turret that another invader has just destroyed
        if(turretObject != null)
        {
            // Get access to Turret script
            turret = turretObject.GetComponent<Turret>();

            // Find x distance to said turret 
            distanceToTurret = Mathf.Abs(turretObject.transform.position.x - gameObject.transform.position.x);

            // Calculate a bool to determine if invader is in range of closest turret
            inRange = weaponRange >= distanceToTurret;
        }
        

        if (inRange)
        {
            Attack();
        }
        else
        {
            Move();
        }

    }

    public bool IsInRange()
    {
        return inRange;
    }

    private void Attack()
    {
        // Play animation and deal damage to turret
        animator.SetBool("inRange", true);
        if(turret != null)
        {
            turret.TakeDamage(damage * Time.deltaTime);
        }
        

    }

    private void Move()
    {
        animator.SetBool("inRange", false);
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

    }

    GameObject GetClosestTurret(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestXDistance = Mathf.Infinity;
        Vector3 currentPosition = gameObject.transform.position;

        foreach (GameObject potentialTarget in enemies)
        {
            float xDistanceToTarget = Mathf.Abs(potentialTarget.transform.position.x - currentPosition.x);
            float yDistanceToTarget = Mathf.Abs(potentialTarget.transform.position.y - currentPosition.y);

            // Find the turret with closest x distance and that is on the right lane
            if (xDistanceToTarget < closestXDistance && yDistanceToTarget < 1f)
            {
                closestXDistance = xDistanceToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurretProjectile")
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            TakeDamage(projectile.GetDamage());
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
  
        // Check if invader has been killed
        if(health <= 0)
        {
            Instantiate(deathSound, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
