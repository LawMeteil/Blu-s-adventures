using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackExample : MonoBehaviour
{
    public int damage = 1;
    public float range = 1f;
    PlayerMovementsExample player;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    Vector2 p;
    public Animator animator;
    string attackAnim;
    bool attacking;
    float attack_counter;
    void Start()
    {
        player = gameObject.GetComponent<PlayerMovementsExample>();
        p = player.transform.position;
        attackAnim = "RightAttack";
    }

    void Update()
    {
        p = player.transform.position;
        attack_counter -= Time.deltaTime;
        if (attack_counter <= 0)
        {
            attacking = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!attacking)
            {
                attacking = true;
                attack_counter = 0.5f;
                Attack();
                animator.SetTrigger(attackAnim);
            }
        }
        if (player.movement.x > 0)
        {
            attackAnim = "RightAttack";
            p = player.transform.position;
            p.x += 0.5f;
        }
        if (player.movement.x < 0)
        {
            attackAnim = "LeftAttack";
            p = player.transform.position;
            p.x -= 0.5f;
        }
        if (player.movement.y > 0)
        {
            p = player.transform.position;
            p.y += 0.5f;
        }
        if (player.movement.y < 0)
        {
            p = player.transform.position;
            p.y -= 1f;
        }
        attackPoint.position = p;
    }

    public void Attack()
    {
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
    }
}
