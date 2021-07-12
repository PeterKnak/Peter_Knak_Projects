using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFly : MonoBehaviour
{
    [SerializeField] float fireballInterval = 1f;
    [SerializeField] GameObject fireball;
    [SerializeField] float airSpeed = 2f;
    bool recharging = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!recharging)
        {
            StartCoroutine(DropFire());
        }
        FindPlayerDirection();
        MoveTowardsPlayer(FindPlayerDirection());
    }

    IEnumerator DropFire()
    {
        recharging = true;
        Instantiate(fireball, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
        yield return new WaitForSeconds(fireballInterval);
        recharging = false;
    }

    float FindPlayerDirection()
    {
        Player player = FindObjectOfType<Player>();
        return Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
    }

    void MoveTowardsPlayer(float direction)
    {
        transform.localScale = new Vector2(direction, 1f);
        transform.position = new Vector2(transform.position.x + Time.deltaTime * airSpeed * direction, transform.position.y);
    }
}
