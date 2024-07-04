using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIntro : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip drop;
    public AudioClip jar;

    // Start is called before the first frame update
    public void PlaySound1()
    {
        audioSource.PlayOneShot(drop);
    }

    public void PlaySound2() {
        audioSource.PlayOneShot(jar);
    }
}
