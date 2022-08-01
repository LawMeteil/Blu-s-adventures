using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Enemy enemy;
    EnemyData enemyData;
    Collider2D areaOfSight;
    Collider2D attackZone;
    public LayerMask playerLayer;
    SpriteRenderer attackZoneSprite;
    GameObject target;
    int attack_speed = 2;
    float count = 0;
    float count_two = 0;
    float count_charging = 0;
    float count_exploding = 0;
    int rafale = 0;
    bool player_damaged = false;
    public GameObject projectile;
    enum boss_states { Slow, Fast, Charging, Explode }
    boss_states state = boss_states.Slow;
    void Start()
    {
        enemyData = gameObject.GetComponent<EnemyData>();
        enemy = enemyData.enemy;
        areaOfSight = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        attackZone = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
        attackZoneSprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (transform.position.x >= target.transform.position.x)
                transform.localScale = new Vector3(-3, 3, 1);
            if (transform.position.x < target.transform.position.x)
                transform.localScale = new Vector3(3, 3, 1);
        }
        if (enemyData.health <= 0)
        {
            enabled = false;
        }
        if (rafale == 4)
        {
            state = boss_states.Charging;
            gameObject.GetComponent<EnemyData>().animator.SetTrigger("Attack_2");
        }
        switch (state)
        {
            case boss_states.Slow:
                slow_state();
                break;
            case boss_states.Fast:
                fast_state();
                break;
            case boss_states.Charging:
                charging_state();
                break;
            case boss_states.Explode:
                explode_state();
                break;
        }
    }

    void slow_state()
    {
        if (target != null)
        {
            count += Time.deltaTime;
            count_two += Time.deltaTime;
            if (count >= 1f / attack_speed)
            {
                gameObject.GetComponent<EnemyData>().animator.SetTrigger("Attack_1");
                count -= 1f / attack_speed;
                GameObject instancedProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
                instancedProjectile.GetComponent<Projectile>().SetDamage(2);
                instancedProjectile.GetComponent<Projectile>().SetDir((target.transform.position - transform.position).normalized);
                instancedProjectile.GetComponent<Projectile>().SetSpeed(15);
            }
            if (count_two >= 3)
            {
                rafale += 1;
                count_two = 0;
                count = 0;
                state = boss_states.Fast;
            }
        }
    }

    void fast_state()
    {
        if (target != null)
        {
            count += Time.deltaTime;
            count_two += Time.deltaTime;
            if (count >= 1f / attack_speed * 0.5)
            {
                gameObject.GetComponent<EnemyData>().animator.SetTrigger("Attack_3");
                count -= 1f / attack_speed * 0.5f;
                GameObject instancedProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
                instancedProjectile.GetComponent<Projectile>().SetDamage(2);
                instancedProjectile.GetComponent<Projectile>().SetDir((target.transform.position - transform.position).normalized);
                instancedProjectile.GetComponent<Projectile>().SetSpeed(15);
            }
            if (count_two >= 3)
            {
                rafale += 1;
                count_two = 0;
                count = 0;
                state = boss_states.Slow;
            }
        }
    }

    void charging_state()
    {
        rafale = 0;
        attackZoneSprite.color = new Color(1, 0, 0, 0.3f);
        count_charging += Time.deltaTime;
        gameObject.transform.GetChild(1).transform.localScale = new Vector2(8 * (count_charging / 8), 8 * (count_charging / 8));
        if (count_charging >= 8)
        {
            count_charging = 0;
            state = boss_states.Explode;
            gameObject.GetComponent<EnemyData>().animator.SetTrigger("Attack_2_Ended");
        }
    }

    void explode_state()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(gameObject.transform.position, 12, playerLayer);
        if (player.Length != 0)
        {
            if (!player_damaged)
            {
                if (player[0].gameObject.GetComponent<PlayerMovementsExample>().attacking == false)
                    player[0].gameObject.GetComponent<PlayerMovementsExample>().animator.SetTrigger("Hurt");
                player[0].gameObject.GetComponent<PlayerActions>().TakeDamage(10);
                player_damaged = true;
            }
        }
        count_exploding += Time.deltaTime;
        attackZoneSprite.color = new Color(1, 0, 0, 1);
        if (count_exploding >= 0.5)
        {
            count_exploding = 0;
            gameObject.transform.GetChild(1).transform.localScale = Vector2.zero;
            state = boss_states.Slow;
            player_damaged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            target = null;
        }
    }
}