using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygeneShop : MonoBehaviour
{
    int price = 10;
    TextMesh priceText;

    private void Start()
    {
        priceText = transform.GetChild(0).GetComponent<TextMesh>();
        priceText.text = price.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            if (player.GetComponent<PlayerData>().coins >= price)
            {
                player.GetComponent<PlayerData>().coins -= price;
                player.GetComponent<PlayerData>().maxO2 += 20;
                player.GetComponent<PlayerData>().O2 += 20;
                GameObject healthBar = PlayerData.healthBar;
                healthBar.GetComponent<HealthBar>().SetMaxHealth(player.GetComponent<PlayerData>().maxO2);
                healthBar.GetComponent<HealthBar>().SetHealth(player.GetComponent<PlayerData>().O2);
                price += 10;
                priceText.text = price.ToString();
                SetShopInactive();
            }
        }
    }

    public void SetShopActive()
    {
        gameObject.SetActive(false);
    }
    public void SetShopInactive()
    {
        gameObject.SetActive(false);
    }
}
