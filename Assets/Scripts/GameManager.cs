using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour   
{
    public int cantMadness;
    public SpawnerGhoul spawn;
    public bool onPause = false; //Verification pause
    [Header("Comandos menu pausa")]
    public GameObject panelPause; //For script GameManager

    public void Awake() //set hide panelPause on Awake
    {
        panelPause.SetActive(false);   
    }

    public void Update() //Keycode for Pause and functions
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (onPause)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < cantMadness; i++)
            {
                spawn.SpawnEnemy();
            }
        }
    }

    public void play() //Function for start game
    {
        SceneManager.LoadScene(2);
    }

    public void Continue() //Function to resume game
    {
        Time.timeScale = 1; // Continue Game
        onPause = false;
        panelPause.SetActive(false);
        AudioListener.pause = false;
    }

    public void Pause() // Function to pause game
    {
        Time.timeScale = 0;//Pause Game
        onPause = true;
        panelPause.SetActive(true);
        AudioListener.pause = true;
    }

    public void Exit() //Function to exit and return the Menu
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); //Load scene menu
    }
}
