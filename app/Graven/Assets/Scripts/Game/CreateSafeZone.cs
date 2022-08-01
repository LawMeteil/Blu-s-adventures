using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSafeZone : MonoBehaviour
{
    Collider2D teleport;
    void Awake()
    {
        GameManager.OnGameStateChanged += CreateSafeZoneOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= CreateSafeZoneOnGameStateChanged;
    }

    void CreateSafeZoneOnGameStateChanged(GameState state)
    {
        if (state == GameState.SafeZone)
        {
            GameObject player = GameManager.Instance.GetPlayer();
            teleport = gameObject.GetComponent<Collider2D>();
            teleport.enabled = true;
            GameManager.Instance.level += 1;
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(-16, 0, -10);
            player.transform.position = new Vector3(-16, -8, -1);
            float n = Random.Range(0f, 4f);
            if (n < 1)
            {
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-16, 5f, -1), Quaternion.identity);
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-16, 6.65f, -1), Quaternion.identity);
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-17.25f, 3.75f, -1), Quaternion.identity);
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-14.75f, 3.75f, -1), Quaternion.identity);
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-17.5f, 5.5f, -1), Quaternion.identity);
                Instantiate(GameManager.Instance.coinPrefab, new Vector3(-14.5f, 5.5f, -1), Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.UpdateGameState(GameState.Game);
            teleport.enabled = false;
        }
    }
}
