using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel4 : MonoBehaviour
{
    Collider2D teleport;

    void Awake()
    {
        GameManager.OnGameStateChanged += CreateLevel4OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateLevel4OnGameStateChanged;
    }

    private void CreateLevel4OnGameStateChanged(GameState state)
    {
        if (state == GameState.Game && GameManager.Instance.level == 4)
        {
            GameObject enemyPrefab = GameManager.Instance.GetEnemyPrefab();
            GameObject player = GameManager.Instance.GetPlayer();
            teleport = gameObject.GetComponent<Collider2D>();
            teleport.enabled = false;
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(0.5f, 65, -10);
            player.transform.position = new Vector3(0, 57, -1);
            Instantiate(enemyPrefab, new Vector3(-4.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-0.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(2.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(4.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(5.5f, 74, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4.5f, 71, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3.5f, 72, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2.5f, 69, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1.5f, 69, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-0.5f, 72, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0.5f, 70, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1.5f, 72, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(2.5f, 69, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3.5f, 71, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(4.5f, 72, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(5.5f, 70, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4.5f, 66, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3.5f, 68, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2.5f, 67, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1.5f, 65, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-0.5f, 67, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0.5f, 66, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1.5f, 66, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-4.5f, 63, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-3.5f, 64, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2.5f, 63, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-1.5f, 62, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-0.5f, 65, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(0.5f, 61, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1.5f, 62, -1), Quaternion.identity);
            gameObject.GetComponent<CreateLevel4>().enabled = true;
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.state == GameState.Game && GameManager.Instance.level == 4)
        {
            if (enemies.Length == 0)
            {
                teleport.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.UpdateGameState(GameState.Boss);
        teleport.enabled = false;
        gameObject.GetComponent<CreateLevel4>().enabled = false;
    }
}
