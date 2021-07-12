using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Paddle : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float unitsInScreenWidth = 16f;
    [SerializeField] float min = 1f;
    [SerializeField] float max = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cursorPosition = Input.mousePosition.x * unitsInScreenWidth / Screen.width;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

        paddlePos.x = Mathf.Clamp(cursorPosition, min, max);

        Vector2 paddleScale = transform.localScale;

        transform.position = paddlePos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            paddleScale.x = (-1 * paddleScale.x);
        }

        transform.localScale = paddleScale;
        
    }
}
