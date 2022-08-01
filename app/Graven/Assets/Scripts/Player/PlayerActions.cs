using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerActions : MonoBehaviour
{
    PlayerData playerData;
    Player player;
    public Animator animator;
    public bool dead;

    private float f;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        playerData = gameObject.GetComponent<PlayerData>();
        player = playerData.player;
        f = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (!playerData.invicible)
            {
                f += Time.deltaTime;
                if (f > 1)
                {
                    playerData.O2 -= 1;
                    f -= 1;
                    PlayerData.healthBar.GetComponent<HealthBar>().SetHealth(playerData.O2);
                }

                if (playerData.O2 <= 0)
                    Die();
            }

            if (playerData.O2 >= playerData.maxO2)
                playerData.O2 = playerData.maxO2;
        }
    }

    public void TakeDamage(int damage)
    {
        playerData.O2 -= damage;
        if (playerData.O2 < 0)
            playerData.O2 = 0;
        PlayerData.healthBar.GetComponent<HealthBar>().SetHealth(playerData.O2);
    }

    void Die()
    {
        dead = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<PlayerAttack>().enabled = false;
        gameObject.GetComponent<PlayerMovements>().enabled = false;
        playerData.animator.SetBool("IsDead", true);
    }

    void Delete()
    {
        gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }
}