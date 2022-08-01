using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel1 : MonoBehaviour
{
    Collider2D teleport;

    void Awake()
    {
        GameManager.OnGameStateChanged += CreateLevel1OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateLevel1OnGameStateChanged;
    }

    private void CreateLevel1OnGameStateChanged(GameState state)
    {
        if (state == GameState.Game && GameManager.Instance.level == 1)
        {
            GameObject enemyPrefab = GameManager.Instance.GetEnemyPrefab();
            GameObject player = GameManager.Instance.GetPlayer();
            gameObject.GetComponent<CreateLevel1>().enabled = true;
            teleport = gameObject.GetComponent<Collider2D>();
            teleport.enabled = false;
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(0, 0, -10);
            player.transform.position = new Vector3(-3.35f, -7.73f, -1);
            Instantiate(enemyPrefab, new Vector3(4.55f, -7.32f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(2.64f, -4.8f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(-2.16f, 0.49f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(1.22f, 3.2f, -1), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(3.56f, 7.87f, -1), Quaternion.identity);
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.state == GameState.Game && GameManager.Instance.level == 1)
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
        gameObject.GetComponent<CreateLevel1>().enabled = false;
    }
}
