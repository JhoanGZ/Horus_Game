using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int cantMadness;
    public SpawnerGhoul spawn;
    public SpawnSlain spawnSlain;
    public bool onPause = false; // Verification pause
    [Header("Comandos menu pausa")]
    public GameObject panelPause; // For script GameManager

    public static int deadGhoulCount = 10; // Start of ghoul count decrease
    public static int targetGhoul = 0; // Reach of 0 ghoul
    public Text targetGhoulText;
    public static GameManager instance;

    void Awake()
    {
        //Instatiate the correct GameManager in this case "Juego"
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start() // Set hide panelPause on Start
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            panelPause.SetActive(false);
            AssignUITextElements();
            UpdateUI();
        }
    }

    public void Update() // Keycode for Pause and functions
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (onPause)
                {
                    Continue();
                }
                else
                {
                    Pause();
                }
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

    public void IncrementGhoulCount()
    {
        deadGhoulCount--;
        UpdateUI();

        if (deadGhoulCount <= 0)
        {
            
            if (deadGhoulCount == 0)
            {
                Debug.Log("Spawning Boss");
                spawnSlain.SpawnBoss();
            }

        }
        else
        {

        }
        
    }

    public void UpdateUI() //Update UI for Kill ghouls
    {
        targetGhoulText.text = deadGhoulCount.ToString();
    }

    public void AssignUITextElements()
    {
        // Ensure the UI Text components are assigned after scene load
        targetGhoulText = GameObject.Find("TargetGhoulText").GetComponent<Text>();
    }

    public void play() // Function for start game
    {
        SceneManager.LoadScene(2);
        AudioListener.pause = false;
    }

    public void Continue() // Function to resume game
    {
        Time.timeScale = 1; // Continue Game
        onPause = false;
        panelPause.SetActive(false);
        AudioListener.pause = false;
    }

    public void Pause() // Function to pause game
    {
        Time.timeScale = 0; // Pause Game
        onPause = true;
        panelPause.SetActive(true);
        AudioListener.pause = true;
    }

    public void ReturnMenu() // Function to exit and return the Menu
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        AudioListener.pause = false; // Load scene menu
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
