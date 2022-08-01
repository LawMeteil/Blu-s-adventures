using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioClip Ambiancesound;
    public AudioClip MusicBoss;

    public AudioSource audioSource;


    public void ChangeMusic()
    {

        if (GameManager.Instance.state == GameState.Boss)
        {
            audioSource.Stop();
            audioSource.clip = MusicBoss;
            audioSource.Play();
        }

    }
}