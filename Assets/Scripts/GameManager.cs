using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
    public GameObject panelUI;
    public Canvas winScreenCanvas;
    public Canvas winImageCanvas;
    public Canvas winCreditsCanvas;
    
    public  int deadGhoulCount = 100; // Start of ghoul count decrease
    public  int targetGhoul = 0; // Reach of 0 ghoul
    public Text targetGhoulText;
    public static GameManager instance;
    public Image transitionImage;
    public Text messageText;

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

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            winImageCanvas.gameObject.SetActive(false);
            StartCoroutine(DisplayMessageShowWinAndCredits("Has ganado en esta oportunidad, pero no escaparas...", 5f));

        }
    }

    public void Start() // Set hide panelPause on Start
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            panelPause.SetActive(false);
            AssignUITextElements();
            InitializeGame();
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

        if (Input.GetKeyDown(KeyCode.N))
        {
            DeadSlain();
        }
    }

    public void IncrementGhoulCount()
    {
        if (deadGhoulCount > 0)
        {
            deadGhoulCount--;
            UpdateUI();

            if (deadGhoulCount == 0)
            {
                Debug.Log("Spawning Boss");
                spawnSlain.SpawnBoss();
            }
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

    public void DeadSlain()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(FadeToWhite());
        }
    }

    private IEnumerator FadeToWhite()
    {
        float duration = 4f; // Duration of the transition
        float elapsed = 0.0f;
        Color color = transitionImage.color;
        panelUI.SetActive(false);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            transitionImage.color = color;
            yield return null;
        }

        // Log a message to confirm that the transition is complete
        SceneManager.LoadScene("Final");
    }

    private IEnumerator DisplayMessageShowWinAndCredits(string message, float delayToShowWin)
    {
        messageText.text = "";
        winScreenCanvas.gameObject.SetActive(true);
        winCreditsCanvas.gameObject.SetActive(false);

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.15f); 
        }

        // Esperar el tiempo especificado antes de mostrar la imagen de ganador
        yield return new WaitForSeconds(delayToShowWin);

        // Desactivar la pantalla de victoria y activar la imagen de ganador
        winScreenCanvas.gameObject.SetActive(false);
        winImageCanvas.gameObject.SetActive(true);

        // Esperar hasta que se haga clic con el mouse para mostrar los créditos
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // Desactivar la imagen de ganador y activar los créditos
        winImageCanvas.gameObject.SetActive(false);
        winCreditsCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        // Esperar hasta que se haga clic con el mouse para cargar la escena del menú
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // Cargar la escena del menú
        SceneManager.LoadScene("Menu");
    }

    private void InitializeGame()
    {
        deadGhoulCount = 5; // Reset the ghoul count to its initial value
        targetGhoul = 0; // Reset the target ghoul count
    }

}
