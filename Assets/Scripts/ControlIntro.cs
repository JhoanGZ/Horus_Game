using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlIntro : MonoBehaviour
{
    public float duration = 3f; //Intro duration
    public int scene = 1; // Scene number to load

    void Start()
    {
        StartCoroutine(IntroChums()); // Call Coroutine
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //For Skip scene
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator IntroChums() 
    {
        yield return new WaitForSeconds(duration); //Coroutine that will wait for a specified number of seconds and then change the scene
        SceneManager.LoadScene(scene); //Scene id for load
    }
}
