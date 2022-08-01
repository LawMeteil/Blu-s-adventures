using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float range;
    PlayerMovements player;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    Vector2 p;
    public Animator animator;
    string attackAnim;
    float attack_counter;
    bool attacking;
    GameObject cursor;
    void Start()
    {
        cursor = gameObject.transform.GetChild(0).gameObject;
        player = gameObject.GetComponent<PlayerMovements>();
        p = player.transform.position;
        range = gameObject.GetComponent<PlayerData>().range;
        attackAnim = "RightAttack";
    }

    void Update()
    {
        attack_counter -= Time.deltaTime;
        if (attack_counter <= 0)
        {
            attacking = false;
        }
        p = player.transform.position;
        p.y += player.direction.y / 2;
        p.x += player.direction.x / 2;

        if (player.direction.y < 0)
        {
            p.y += player.direction.y / 2;
        }
        attackPoint.position = p;
    }

    public void Attack()
    {
        if (Input.touchCount == 1)
        {
            if (!attacking)
            {
                attacking = true;
                attack_counter = 0.5f;
                animator.SetTrigger(attackAnim);
                if (player.direction.x > 0)
                {
                    attackAnim = "RightAttack";
                }
                if (player.direction.x < 0)
                {
                    attackAnim = "LeftAttack";
                }

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
                if (hitEnemies.Length == 1)
                {
                    hitEnemies[0].GetComponent<EnemyCollision>().TakeDamage(damage);
                }
                else if (hitEnemies.Length > 1)
                {
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        if (enemy.gameObject.name.Contains("Floppy"))
                        {
                            enemy.gameObject.GetComponent<EnemyCollision>().TakeDamage(damage);
                            break;
                        }
                    }
                    hitEnemies[0].GetComponent<EnemyCollision>().TakeDamage(damage);
                }
                gameObject.GetComponent<PlayerMovements>().state = PlayerMovements.states.Staying;
            }
        }
    }
}