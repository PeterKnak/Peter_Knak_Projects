using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
 
    [SerializeField] int numberOfBalls;

    int lostBalls = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lostBalls++;
        if(lostBalls >= numberOfBalls)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
    

}
