using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSelector : MonoBehaviour
{
    [SerializeField] int position;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ReceiveInput());
        DetermineLane();
    }

    private void DetermineLane()
    {
        if (position == 0)
        {
            transform.position = new Vector3(-0.4f, -4.16f, -1);
        }
        if (position == 1)
        {
            transform.position = new Vector3(-0.4f, -2.37f, -1);
        }
        if (position == 2)
        {
            transform.position = new Vector3(-0.4f, -0.58f, -1);
        }
    }

    IEnumerator ReceiveInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            position = Mathf.Clamp(position + 1, 0, 2);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            position = Mathf.Clamp(position - 1, 0, 2);
        }

        // There is a slight delay to prevent the lane selector moving up or down multiple lanes at once.
        yield return new WaitForSeconds(0.1f);

    }

    public int GetLanePosition()
    {
        return position;
    }

}
