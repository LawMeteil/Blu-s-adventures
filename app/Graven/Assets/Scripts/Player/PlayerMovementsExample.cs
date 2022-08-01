using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsExample : MonoBehaviour
{
    public Transform attackPoint;
    public float range = 1f;
    public LayerMask enemyLayers;
    public float MovementSpeed = 3;
    public int damage = 1;
    public Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator;
    public bool attacking;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (attacking)
            movement = Vector2.zero;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * MovementSpeed * Time.fixedDeltaTime);
    }

    public void ResetAttack()
    {
        attacking = false;
    }
}