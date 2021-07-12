using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    [SerializeField] float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
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
