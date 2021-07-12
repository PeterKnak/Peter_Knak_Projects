using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] int currentLevel = 1;


    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGameOver(int currentLevel)
    {
        string level = "Lose Screen " + currentLevel;
        SceneManager.LoadScene(level);
    }

    public void LoadLevelLost()
    {
        Level level = FindObjectOfType<Level>();
        currentLevel = level.GetCurrentLevel();
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("How To Play");
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level 5");
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene("Level 6");
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene("Level 7");
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene("Level 8");
    }
    public void LoadLevel9()
    {
        SceneManager.LoadScene("Level 9");
    }
    public void LoadLevel10()
    {
        SceneManager.LoadScene("Level 10");
    }
}
