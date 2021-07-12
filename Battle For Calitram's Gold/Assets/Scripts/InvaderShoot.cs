using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShoot : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Invader invader;
    [SerializeField] float projectileFiringPeriod = 1;
    [SerializeField] Quaternion projectileQuaternion = new Quaternion(0f, 0f, 0f, 0);

    bool allowFire = true;

    void Update()
    {
        if (invader.IsInRange())
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (allowFire)
        {
            StartCoroutine(FireContinuously());
        }

    }

    IEnumerator FireContinuously()
    {
        allowFire = false;
        Instantiate(projectile, new Vector2(transform.position.x + 0.3f, transform.position.y), projectileQuaternion);

        // Wait for some interval before firing again
        yield return new WaitForSeconds(projectileFiringPeriod + Random.Range(0, 0.5f));
        allowFire = true;
    }
}
