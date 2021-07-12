using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField] GameObject boss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(FindObjectOfType<Boss>().IsAlive())
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
