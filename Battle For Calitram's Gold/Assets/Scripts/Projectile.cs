using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    // Update is called once per frame
    void Update()
    {
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
