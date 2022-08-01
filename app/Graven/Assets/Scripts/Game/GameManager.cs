using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public int level;
    float global_timer;
    public static event Action<GameState> OnGameStateChanged;
    public GameObject musicManager;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject attackShopPrefab;
    public GameObject oxygeneShopPrefab;
    public GameObject coinPrefab;
    public GameObject bubblePrefab;
    GameObject player;
    GameObject attackShop;
    GameObject oxygeneShop;
    private void Awake()
    {
        Instance = this;
        player = Instantiate(playerPrefab, new Vector3(0, 0, -1), Quaternion.identity);
        attackShop = Instantiate(attackShopPrefab, new Vector3(-18, 7, -1), Quaternion.identity);
        oxygeneShop = Instantiate(oxygeneShopPrefab, new Vector3(-13, 5, -1), Quaternion.identity);
        attackShop.SetActive(false);
        oxygeneShop.SetActive(false);
    }

    private void Start()
    {
        level = 1;
        UpdateGameState(GameState.Game);
    }

    private void Update()
    {
        if (state != GameState.Victory)
            global_timer += Time.deltaTime;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.Game:
                HandleGame();
                break;
            case GameState.SafeZone:
                HandleSafeZone();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Boss:
                HandleBoss();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void HandleGame()
    {
        player.GetComponent<PlayerData>().invicible = false;
        attackShop.SetActive(false);
        oxygeneShop.SetActive(false);
    }

    public void HandleSafeZone()
    {
        player.GetComponent<PlayerData>().O2 = player.GetComponent<PlayerData>().maxO2;
        PlayerData.healthBar.GetComponent<HealthBar>().SetHealth(player.GetComponent<PlayerData>().maxO2);
        player.GetComponent<PlayerData>().invicible = true;
    }

    public void HandleVictory()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        GameObject.Find("Main Camera").transform.position = new Vector3(-32, 0, -10);
        player.GetComponent<PlayerData>().invicible = false;
        GameObject.Find("Canvas").SetActive(false);
        GameObject.Find("Victory screen/Your score").GetComponent<TextMesh>().text = "Your score" + '\n' + TimeSpan.FromSeconds(global_timer).TotalSeconds.ToString("0.0");
        if (!PlayerPrefs.HasKey("Highscore"))
            PlayerPrefs.SetFloat("Highscore", global_timer);
        if (PlayerPrefs.GetFloat("Highscore") > global_timer)
            PlayerPrefs.SetFloat("Highscore", global_timer);
        GameObject.Find("Victory screen/Best score").GetComponent<TextMesh>().text = "Best score" + '\n' + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Highscore")).TotalSeconds.ToString("0.0");
    }

    public void HandleLose()
    {
        level = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        ReactivatePlayer();
        attackShop.SetActive(true);
        oxygeneShop.SetActive(true);
        UpdateGameState(GameState.SafeZone);
    }

    public void HandleLevel1()
    {
    }

    public void HandleLevel2()
    {
    }
    public void HandleLevel3()
    {
    }
    public void HandleLevel4()
    {
    }
    public void HandleBoss()
    {
        player.GetComponent<PlayerData>().Refresh();
        musicManager.transform.GetChild(0).GetComponent<Music>().ChangeMusic();
    }

    public void ReactivatePlayer()
    {
        player.SetActive(true);
        player.GetComponent<PlayerData>().animator.Play("Player_Idle");
        player.GetComponent<PlayerData>().Refresh();
        player.GetComponent<PlayerActions>().dead = false;
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<PlayerAttack>().enabled = true;
        player.GetComponent<PlayerMovements>().enabled = true;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetBossPrefab()
    {
        return bossPrefab;
    }

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public GameObject GetBubblePrefab()
    {
        return bubblePrefab;
    }

    public float GetGlobalTimer()
    {
        return global_timer;
    }
}

public enum GameState
{
    Game,
    Victory,
    Lose,
    SafeZone,
    Boss
}