using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreen;
    public float Duration = 2f;


    // Start is called before the first frame update
    void Start()
    {
        blackScreen.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeToScene(string nameScene)
    {
        Debug.Log("FadeToScene called with scene: " + nameScene);
        StartCoroutine(DarkIn(nameScene));
    }

    private IEnumerator DarkIn(string nameScene)
    {
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            blackScreen.color = new Color(0f, 0f, 0f,  elapsedTime / Duration);
            yield return null;
        }

        blackScreen.color = new Color(0f,0f,0f,1f);
        Debug.Log("Loading scene: " + nameScene);
        SceneManager.LoadScene(nameScene);
    }
}
