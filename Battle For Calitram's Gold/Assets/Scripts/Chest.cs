using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool isColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "InvaderProjectile" && collision.gameObject.tag != "TurretProjectile")
        {
            // Make sure the chest can only be picked up once
            if (isColliding)
            {
                return;
            }
            isColliding = true;
            Destroy(gameObject);
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.ChestPickUp();

        }

    }
}
