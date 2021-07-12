using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //Configuration Parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparksVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites; 

    //Cached References
    Level level;

    //State Varaiables 
    [SerializeField] int timesHit; //Only Serialized for debug purposes

    private void Start()
    {
        if(tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.CountBreakableBlocks();
        }
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {

        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        if(hitSprites[timesHit-1] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array : " + gameObject.name);
        }

    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        level.BlockDestroyed();
        FindObjectOfType<GameSession>().AddToScore();
        TriggerSparksVFX();
    }

    private void TriggerSparksVFX()
    {
        GameObject sparks = Instantiate(blockSparksVFX, transform.position, transform.rotation);
        Destroy(sparks, 1.0f);
    }

}
