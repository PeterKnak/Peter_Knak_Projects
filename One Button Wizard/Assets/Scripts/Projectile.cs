using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] Vector2 originalScale;
    [SerializeField] Animator myAnimator;
    [SerializeField] float projectileDamage = 20f;

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + new Vector2(projectileSpeed * Time.fixedDeltaTime, 0f));
    }

    public void SetProjectileSpeed(float newSpeed, float direction)
    {
        projectileSpeed = newSpeed * direction;
        transform.localScale = new Vector2(originalScale.x * direction, originalScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(projectileDamage);
        }
        myAnimator.SetTrigger("hitEnemy");

    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
