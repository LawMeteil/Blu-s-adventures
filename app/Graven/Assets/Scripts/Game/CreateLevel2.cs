using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel2 : MonoBehaviour
{
    Collider2D teleport;

    void Awake()
    {
        GameManager.OnGameStateChanged += CreateLevel2OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateLevel2OnGameStateChanged;
    }

    private void CreateLevel2OnGameStateChanged(GameState state)
    {
        if (state == GameState.Game && GameManager.Instance.level == 2)
        {
            GameObject enemyPrefab = GameManager.Instance.GetEnemyPrefab();
            GameObject player = GameManager.Instance.GetPlayer();
            teleport = gameObject.GetComponent<Collider2D>();
            teleport.enabled = false;
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(0, 21, -10);
            player.transform.position = new Vector3(-5, 13, -1);
            Instantiate(enemyPrefab, new Vector3(-2, 19, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3, 20, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4, 19, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1, 22, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2, 24, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3, 23, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4, 22, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(4, 27, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(4, 29, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3, 31, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(2, 30, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3, 29, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(4, 31, -1), Quaternion.identity);
            gameObject.GetComponent<CreateLevel2>().enabled = true;
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.state == GameState.Game && GameManager.Instance.level == 2)
        {
            if (enemies.Length == 0)
            {
                teleport.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.UpdateGameState(GameState.SafeZone);
        teleport.enabled = false;
        gameObject.GetComponent<CreateLevel2>().enabled = false;
    }
}
