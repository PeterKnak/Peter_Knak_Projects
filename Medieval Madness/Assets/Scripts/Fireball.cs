using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] float damage = 20;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0;
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<Player>().TakeDamage(damage);
            FindObjectOfType<GameSession>().UpdatePlayerSlider(damage);
        }
        animator.SetTrigger("explode");
        StartCoroutine(DestroyFireball());
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
