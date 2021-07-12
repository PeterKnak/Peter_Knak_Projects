using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this object collides with the player, win the game.
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<SceneLoader>().LoadWinScreen();
        }
        
    }
}
