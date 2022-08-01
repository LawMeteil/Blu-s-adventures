using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBossLevel : MonoBehaviour
{
    GameObject enemyPrefab;
    GameObject bubblePrefab;
    float floppy_timer = 8;
    int i = 0;
    Vector3[] pos = new Vector3[] {
        new Vector3(-3.5f, 90, -1),
        new Vector3(-2.5f, 88, -1),
        new Vector3(0.5f, 86, -1),
        new Vector3(3.5f, 88, -1),
        new Vector3(4.5f, 90, -1)
        };

    void Awake()
    {
        GameManager.OnGameStateChanged += CreateBossLevelOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateBossLevelOnGameStateChanged;
    }

    private void CreateBossLevelOnGameStateChanged(GameState state)
    {
        if (state == GameState.Boss)
        {
            enemyPrefab = GameManager.Instance.GetEnemyPrefab();
            bubblePrefab = GameManager.Instance.GetBubblePrefab();
            GameObject player = GameManager.Instance.GetPlayer();
            GameObject bossPrefab = GameManager.Instance.GetBossPrefab();
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(0.5f, 85.5f, -10);
            player.transform.position = new Vector3(0.5f, 76, -1);
            Instantiate(bossPrefab, new Vector3(0.5f, 92, -1), Quaternion.identity);
            gameObject.GetComponent<CreateBossLevel>().enabled = true;
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (GameManager.Instance.state == GameState.Boss)
        {
            bool found = false;
            foreach (GameObject enemy in enemies)
            {
                if (enemy.name == "Boss(Clone)")
                    found = true;
            }
            if (!found)
                GameManager.Instance.UpdateGameState(GameState.Victory);
            floppy_timer += Time.deltaTime;
            if (floppy_timer >= 6)
            {
                Instantiate(enemyPrefab, pos[i], Quaternion.identity);
                Instantiate(bubblePrefab, pos[i], Quaternion.identity);
                i++;
                if (i > 4)
                    i = 0;
                floppy_timer -= 6;
            }
        }
    }
}
