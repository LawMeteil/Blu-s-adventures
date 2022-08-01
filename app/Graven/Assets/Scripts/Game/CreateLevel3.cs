using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel3 : MonoBehaviour
{
    Collider2D teleport;

    void Awake()
    {
        GameManager.OnGameStateChanged += CreateLevel3OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateLevel3OnGameStateChanged;
    }

    private void CreateLevel3OnGameStateChanged(GameState state)
    {
        if (state == GameState.Game && GameManager.Instance.level == 3)
        {
            GameObject enemyPrefab = GameManager.Instance.GetEnemyPrefab();
            GameObject player = GameManager.Instance.GetPlayer();
            teleport = gameObject.GetComponent<Collider2D>();
            teleport.enabled = false;
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(0, 43, -10);
            player.transform.position = new Vector3(-1.5f, 34, -1);
            Instantiate(enemyPrefab, new Vector3(-4, 39, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4, 40, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2, 42, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1, 43, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0, 44, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0, 45, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0, 43, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1, 44, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3.5f, 44.5f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3, 48, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1.5f, 48.5f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3, 48, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3.5f, 46, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4.75f, 44.5f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0, 47, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0.5f, 46, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1, 46, -1), Quaternion.identity);
            gameObject.GetComponent<CreateLevel3>().enabled = true;
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.state == GameState.Game && GameManager.Instance.level == 3)
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
        gameObject.GetComponent<CreateLevel3>().enabled = false;
    }
}
