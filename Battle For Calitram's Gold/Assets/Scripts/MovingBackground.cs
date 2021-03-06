using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    [SerializeField] float speed = 0.1f;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

        // Once the boat reaches the edge of the screen, it respawns on the other side.
        if(transform.position.x > 10)
        {
            transform.position = new Vector2(-10f, transform.position.y);
        }
        if (transform.position.x < -10)
        {
            transform.position = new Vector2(10f, transform.position.y);
        }
    }
}
