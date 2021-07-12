using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTorrent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(100f);
        }
    }
}
