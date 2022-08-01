using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Pickable
{
    public GameObject Object;
    int lifeTime = 5;
    float count = 0f;
    public void OnPick(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        player.GetComponent<PlayerData>().O2 += 2;
        if (player.GetComponent<PlayerData>().O2 > player.GetComponent<PlayerData>().maxO2)
            player.GetComponent<PlayerData>().O2 = player.GetComponent<PlayerData>().maxO2;
        PlayerData.healthBar.GetComponent<HealthBar>().SetHealth(player.GetComponent<PlayerData>().O2);
        Destroy(gameObject);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= lifeTime)
            Destroy(gameObject);
    }
}