using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Vector2 startPos;
    public Vector2 direction;
    Vector2 newDirection;
    PlayerAttack player;
    public Rigidbody2D rb;
    public Animator animator;
    bool start = false;
    public enum states { Moving, Staying, Swiping };
    public states state;
    float f;
    public float range = 1f;
    public float MovementSpeed;
    public bool attacking;

    void Awake()
    {
        state = states.Staying;
        player = gameObject.GetComponent<PlayerAttack>();
        MovementSpeed = gameObject.GetComponent<PlayerData>().movementSpeed;
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    start = true;
                    f = 0;
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    MovementSpeed = gameObject.GetComponent<PlayerData>().movementSpeed;

                    switch (state)
                    {
                        case states.Moving:
                            if (DeadZone(touch.position, direction * 15) == false)
                            {
                                direction = touch.position - (startPos - direction * 2);
                                direction.Normalize();
                            }
                            break;

                        case states.Staying:
                            direction = touch.position - startPos;
                            direction.Normalize();
                            if (f > 0.15)
                            {
                                state = states.Moving;
                            }
                            break;
                        case states.Swiping:
                            if(f > 0.15)
                            {
                                direction = touch.position - startPos;
                                state = states.Moving;
                            }
                            break;
                    }
                    break;

                case TouchPhase.Stationary:

                    if (state == states.Moving)
                    {
                        startPos = touch.position - direction;
                    }
                    break;

                case TouchPhase.Ended:
                    if (f < 0.15)
                    {
                        state = states.Swiping;
                        player.Attack();
                    }
                    state = states.Staying;
                    MovementSpeed = 0;
                    startPos = Vector2.zero;
                    start = false;
                    break;
            }
        }

        if (start == true)
        {
            f += Time.deltaTime;
        }



        if (state == states.Moving)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
            rb.MovePosition(rb.position + direction * MovementSpeed * Time.fixedDeltaTime);
        }

        if (Input.touchCount == 0)
        {
            MovementSpeed = 0;
            animator.SetFloat("Speed", 0);
        }
    }

    private bool DeadZone(Vector2 touch, Vector2 radius)
    {
        if (touch.x > startPos.x - radius.x
        && touch.x < startPos.x + radius.x
        && touch.x > startPos.y - radius.y
        && touch.y < startPos.y + radius.y)
        {
            return true;
        }
        return false;
    }
}
