using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyData))]
public class FloppyAttack : MonoBehaviour
{
    Enemy enemy;
    EnemyData enemyData;
    enum states { Waiting, Attacking }
    private states state;
    public Collision2D target;

    // Start is called before the first frame update
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemy = enemyData.enemy;
        state = states.Waiting;
        enemyData.attackCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case states.Waiting:
                {
                    waiting_state();
                    break;
                }
            case states.Attacking:
                {
                    attacking_state();
                    break;
                }
        }
    }

    void waiting_state()
    {
    }

    void attacking_state()
    {
        enemyData.attackCooldown -= Time.deltaTime;
        if (enemyData.attackCooldown <= 0)
        {
            enemyData.attackCooldown = enemy.attackCooldown;
            Attack();
        }
    }

    public void Attack()
    {
        target.gameObject.GetComponent<PlayerActions>().TakeDamage(enemyData.damage);
        if (target.gameObject.GetComponent<PlayerMovementsExample>().attacking == false)
            target.gameObject.GetComponent<PlayerMovementsExample>().animator.SetTrigger("Hurt");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemyData.movementSpeed = 0;
            SetState("attack");
            target = collision;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == target)
        {
            enemyData.movementSpeed = enemy.movementSpeed;
            SetState("waiting");
            target = null;
        }
    }

    public void SetState(string s)
    {
        switch (s)
        {
            case "attack":
                {
                    state = states.Attacking;
                    break;
                }
            case "waiting":
                {
                    state = states.Waiting;
                    break;
                }
        }
    }
}
