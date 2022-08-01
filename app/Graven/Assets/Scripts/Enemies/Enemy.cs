using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int health;
    public int damage;
    public int movementSpeed;
    public float attackCooldown;
}
