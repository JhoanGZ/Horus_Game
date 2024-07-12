using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlyanLife : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public UnityEvent<int> setHealth;
    public float invulnerabilityDuration = 1.5f;
    public Image damageImage; // Referencia a la Image en el Canvas
    public float flashSpeed = 5f; // Velocidad a la que se desvanece el color
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Color del flash (rojo con transparencia)
    private bool damaged = false;

    private bool live = true;

    void Start()
    {
        currentHealth = maxHealth;
        setHealth.Invoke(currentHealth);
    }

    private void Update()
    {
        if (damaged)
        {
            // Flash the damageImage
            damageImage.color = flashColor;
        }
        else
        {
            // Fade out the damageImage
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public bool Damaged(int damageAmount)
    {
        damaged = true;

        int tempHealth = currentHealth - damageAmount;
        currentHealth = Mathf.Max(tempHealth, 0);
        
        setHealth.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Dead();
            live = false;
            return live;
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
            live = true;
            return live;
        }
    }

    public void SlyanHeal(int healAmount)
    {
        int tempHealth = currentHealth + healAmount;
        currentHealth = Mathf.Min(tempHealth, maxHealth);
        
        setHealth.Invoke(currentHealth);
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        // Aquí puedes agregar lógica específica para Slyan si es necesario
        yield return new WaitForSeconds(invulnerabilityDuration);
    }

    private void Dead()
    {
        SceneManager.LoadScene("End");
    }
}
