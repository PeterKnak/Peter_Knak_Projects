using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int currentLevel;
    public static bool alreadyInstance = false;

    private void Awake()
    {
        SetUpSingleton();

    }

    private void SetUpSingleton()
    {
        if (!alreadyInstance)
        {
            DontDestroyOnLoad(gameObject);
            alreadyInstance = true;
        }
        else
        {
            Destroy(gameObject);
        } 
            
        
        
    }

    private void Start()
    {
        alreadyInstance = false;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }


}
