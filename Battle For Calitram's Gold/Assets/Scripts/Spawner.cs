using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] LaneSelector laneSelector;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject knight;
    [SerializeField] GameObject cowboy;
    [SerializeField] GameObject wizard;
    [SerializeField] GameObject tank;
    [SerializeField] GameObject truck;
    [SerializeField] int money = 200;
    [SerializeField] int totalChests;
    [SerializeField] GameObject moneySound;
    [SerializeField] int currentLevel;

    int chestsCollected = 0;
    int lane = 0;

    private void Update()
    {
        hasPlayerLost();
    }

    public void hasPlayerLost()
    {
        GameObject[] totalInvaders = GameObject.FindGameObjectsWithTag("Invader");
        if (money <= 5 && totalInvaders.Length == 0)
        {
            SceneLoader sceneloader = FindObjectOfType<SceneLoader>();
            sceneloader.LoadGameOver(currentLevel);
        }
    }

    public void ChestPickUp()
    {
        money += 100;
        chestsCollected++;
        Instantiate(moneySound, transform.position, Quaternion.identity);
        if (chestsCollected >= totalChests)
        {
            WinLevel();
        }
    }

    public void WinLevel()
    {
        SceneLoader sceneloader = FindObjectOfType<SceneLoader>();
        sceneloader.LoadNextLevel();
    }

    public void SpawnSoldier()
    {
        if (money >= 10)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(soldier, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 10;
        }
        
    }
    public void SpawnKnight()
    {
        if (money >= 25)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(knight, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 25;
        }
        
    }
    public void SpawnCowboy()
    {
        if (money >= 30)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(cowboy, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 30;
        }
        
    }
    public void SpawnWizard()
    {
        if (money >= 45)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(wizard, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 45;
        }
        
    }
    public void SpawnTank()
    {
        if (money >= 70)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(tank, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 70;
        }
        
    }
    public void SpawnTruck()
    {
        if(money >= 80)
        {
            float random = Random.Range(-0.2f, 0.2f);
            lane = laneSelector.GetLanePosition();
            float YPos = random - 4.16f + (1.79f * lane);
            Instantiate(truck, new Vector2(-9f, YPos), Quaternion.identity);
            money -= 80;
        }
    }

    public int GetMoney()
    {
        return money;
    }

}
