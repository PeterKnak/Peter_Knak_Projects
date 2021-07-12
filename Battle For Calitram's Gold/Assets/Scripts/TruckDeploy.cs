using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDeploy : MonoBehaviour
{
    [SerializeField] GameObject soldier;
    [SerializeField] float deployDelay = 0.6f;
    bool deployRefresh = true;

    // Update is called once per frame
    void Update()
    {
        if (deployRefresh)
        {
            StartCoroutine(deploy());
        }
    }

    IEnumerator deploy()
    {
        deployRefresh = false;
        Vector2 deployPosition = new Vector2(transform.position.x - 0.5f, transform.position.y + Random.Range(-0.2f, 0.2f));
        Instantiate(soldier, deployPosition, Quaternion.identity);
        yield return new WaitForSeconds(deployDelay);
        deployRefresh = true;
    }
}
