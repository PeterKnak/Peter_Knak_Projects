using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    Slider playerHealth;

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponentInChildren<Slider>();
        UpdatePlayerSlider(-100);
        livesText.text = playerLives.ToString();
    }

    public void UpdatePlayerSlider(float lostHealth)
    {
        if(lostHealth > playerHealth.value)
        {
            playerHealth.value = 0;
        }
        else
        {
            playerHealth.value -= lostHealth;
        }
              
    }
       
    public void ProcessPlayerDeath()
    {
        StartCoroutine(WaitThenDie());
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(1);
        if (playerLives > 1)
        {
            LoseLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void LoseLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        playerLives = 3;
        SceneManager.LoadScene(0);
        livesText.text = playerLives.ToString();
        Destroy(gameObject);
    }
}
