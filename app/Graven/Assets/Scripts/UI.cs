using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        Text health = gameObject.transform.GetChild(1).GetComponent<Text>();
        Text attack = gameObject.transform.GetChild(2).GetComponent<Text>();
        Text coins = gameObject.transform.GetChild(3).GetComponent<Text>();
        Text globalTimer = gameObject.transform.GetChild(4).GetComponent<Text>();
        health.text = player.GetComponent<PlayerData>().O2.ToString();
        attack.text = "Attack: " + player.GetComponent<PlayerData>().damage.ToString();
        coins.text = "Coins: " + player.GetComponent<PlayerData>().coins.ToString();
        float global_timer = GameManager.Instance.GetGlobalTimer();
        globalTimer.text = TimeSpan.FromSeconds(global_timer).TotalSeconds.ToString("0.0");
    }
}
