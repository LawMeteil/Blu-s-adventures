using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public int O2;
    public int coins;
    public float movementSpeed;
    public int damage;
    public float range;
}

