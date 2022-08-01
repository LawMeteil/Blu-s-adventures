using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int speed;
    int damage;
    Vector2 dir;
    float lifeTime = 1.5f;
    float count = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        Vector2 p = gameObject.transform.position;
        p += dir * speed * Time.deltaTime;
        gameObject.transform.position = p;
        if (count >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovementsExample>().attacking == false)
                collision.gameObject.GetComponent<PlayerMovementsExample>().animator.SetTrigger("Hurt");
            collision.gameObject.GetComponent<PlayerActions>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
    }
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }
}
