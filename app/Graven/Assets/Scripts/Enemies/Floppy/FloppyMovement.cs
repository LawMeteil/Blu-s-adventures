using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyMovement : MonoBehaviour
{
    Enemy enemy;
    EnemyData enemyData;
    public Collider2D areaOfSight;
    GameObject target;
    enum states { Idle, Chasing }
    private states state;
    Vector2 pause;
    private void Start()
    {
        state = states.Idle;
        enemyData = gameObject.GetComponent<EnemyData>();
        enemy = enemyData.enemy;
    }

    void Update()
    {
        switch (state)
        {
            case states.Idle:
                {
                    idle_state();
                    break;
                }
            case states.Chasing:
                {
                    chase_state();
                    break;
                }
        }
    }

    private void idle_state()
    {

    }

    private void chase_state()
    {
        if (target != null)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyData.movementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            target = trigger.gameObject;
            state = states.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            target = null;
            state = states.Idle;
        }
    }
}
