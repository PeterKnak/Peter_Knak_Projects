using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    void Update()
    {
        // The projectile moves forward. It will have a collider on it that will trigger when it hits an invader.
        Move();
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

    }

    public int GetDamage()
    {
        return damage;
    }

}
