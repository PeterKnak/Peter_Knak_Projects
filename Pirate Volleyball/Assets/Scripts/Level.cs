using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int numberOfBlocks;

    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        numberOfBlocks++;
    }

    public void BlockDestroyed()
    {
        numberOfBlocks--;
        if (numberOfBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

}
