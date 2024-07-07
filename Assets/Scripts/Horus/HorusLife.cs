using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorusLife : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public UnityEvent<int> setHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        setHealth.Invoke(currentHealth);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HorusTakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HorusHeal(1);
        }
    }

    // Update is called once per frame
    public void HorusTakeDamage(int cantDamage){
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
            Destroy(gameObject);
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
}
