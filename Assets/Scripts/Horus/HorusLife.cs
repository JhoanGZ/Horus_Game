using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HorusLife : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public UnityEvent<int> setHealth;
    public float invulnerabilityDuration = 1.5f;
    public Image damageImage; // Referencia a la Image en el Canvas
    public float flashSpeed = 5f; // Velocidad a la que se desvanece el color
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); // Color del flash (rojo con transparencia)
    private bool damaged = false;



    // Start is called before the first frame update
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

    // Update is called once per frame
    public void HorusTakeDamage(int cantDamage){
        Horus horus = GetComponent<Horus>();
        if(horus != null && horus.IsInvulnerable()) return;  //in invulnerable stop this function

        damaged = true;

        int temporalHealth = currentHealth - cantDamage;
        if (temporalHealth < 0){
            currentHealth = 0;
        }
        else
        {
            currentHealth = temporalHealth;

        }

        setHealth.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    public void HorusHeal(int cantHeal)
    {
        int tempHealth = currentHealth + cantHeal;

        if(tempHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = tempHealth;
        }
        setHealth.Invoke(currentHealth);

    }

   public IEnumerator InvulnerabilityCoroutine() //Coroutine for set invulnerability 
    {
        Horus horus = GetComponent<Horus>();
        if (horus != null)
        {
            horus.SetInvulnerable(true);
        }
        yield return new WaitForSeconds(invulnerabilityDuration);
        if (horus != null)
        {
            horus.SetInvulnerable(false);
        }
    }

    private void Dead()
    {
        SceneManager.LoadScene("Death");
    }
}
