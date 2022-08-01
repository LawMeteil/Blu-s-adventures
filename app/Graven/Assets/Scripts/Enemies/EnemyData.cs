using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Enemy enemy;
    public int health;
    public int damage;
    public int movementSpeed;
    public float attackCooldown;
    public Animator animator;
    void Start()
    {
        health = enemy.health;
        damage = enemy.damage;
        movementSpeed = enemy.movementSpeed;
    }
}
