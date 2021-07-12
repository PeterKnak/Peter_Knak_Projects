using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject fireTorrent;
    [SerializeField] Animator myAnimator;
    [SerializeField] Transform projectileSpawnTransform;
    [SerializeField] float fireballSpeed = 5f;
    [SerializeField] PlayerHealth healthScript;
    
    int spellChoice = 0;

    public void CastCurrentSpell()
    {
        Time.timeScale = 1;

        // Choose a random number from 1 to 3, each corresponding to a spell. 
        spellChoice = Random.Range(1, 4);

        // Play the appropriate casting animation.
        myAnimator.SetInteger("SpellCast", spellChoice);

    }

    public void ResetSpell()
    {
        // This is run at the end of each casting animation.

        spellChoice = 0;
        myAnimator.SetInteger("SpellCast", spellChoice);
        Destroy(FindObjectOfType<FlameTorrent>().gameObject);
    }

    public void ShootFireball()
    {
        float directionOfShot = Mathf.Sign(transform.localScale.x);
        GameObject projectile = Instantiate(fireball, projectileSpawnTransform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetProjectileSpeed(fireballSpeed, directionOfShot);
    }

    public void CastFlameTorrent()
    {
        Instantiate(fireTorrent, transform.position, Quaternion.identity);
    }

    public void HealSelf()
    {
        healthScript.Heal();
    }

    
}
