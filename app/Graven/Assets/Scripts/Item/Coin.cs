using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickable
{
    public GameObject Object;
    int lifeTime = 5;
    float count = 0f;
    public void OnPick(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        player.GetComponent<PlayerData>().coins += 1;
        Destroy(gameObject);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= lifeTime)
            Destroy(gameObject);
    }
}
