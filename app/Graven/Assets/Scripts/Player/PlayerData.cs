using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class PlayerData : MonoBehaviour
{
    public Player player;

    public int maxO2;
    public int O2;
    public int coins;
    public float movementSpeed;
    public int damage;
    public float range;
    public Animator animator;
    public bool invicible;
    static public GameObject healthBar;

    void Start()
    {
        invicible = false;
        maxO2 = player.O2;
        O2 = maxO2;
        coins = player.coins;
        movementSpeed = player.movementSpeed;
        damage = player.damage;
        range = player.range;
        healthBar = GameObject.Find("Canvas/Health bar");
        healthBar.GetComponent<HealthBar>().SetMaxHealth(player.O2);
    }

    public void Refresh()
    {
        O2 = maxO2;
    }
}