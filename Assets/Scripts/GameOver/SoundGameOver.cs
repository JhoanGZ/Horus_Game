using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameOver : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip laughter;
    public AudioClip gameOverTheme;


    // Start is called before the first frame update
    public void PlayLaughter()
    {
        audioSource.PlayOneShot(laughter);
    }

    public void Theme()
    {
        audioSource.PlayOneShot(gameOverTheme);
    }
} 