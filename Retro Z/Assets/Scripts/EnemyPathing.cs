using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    Transform target;
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    float distanceFromPlayer = 0;

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (target != null)
        {
            distanceFromPlayer = Math.Abs(transform.position.y - target.transform.position.y);
        }
        if (distanceFromPlayer > 2 || target == null )
        {
            if (waypointIndex <= waypoints.Count - 1)
            {

                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementThisFrame);
        }
    }
}
