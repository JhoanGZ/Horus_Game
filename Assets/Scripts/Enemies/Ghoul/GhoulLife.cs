using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhoulLife : MonoBehaviour
{
    public int ghoulMaxLife;
    public int ghoulCurrentLife;
    GameObject ghoulObject;
    private bool isDead = false; 


    void Start()
    {
        ghoulObject = this.gameObject; 
        ghoulCurrentLife = ghoulMaxLife;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)){
            GhoulTakeDamage(1);
        }
        
    }

    public void GhoulTakeDamage(int damage) {
        int temporalHealth = ghoulCurrentLife - damage;
        if (temporalHealth < 0)
        {
            ghoulCurrentLife = 0;
        }
        else
        {
            ghoulCurrentLife = temporalHealth;

        }
        if (ghoulCurrentLife <= 0)
        {
            DeadGhoul();
        }
    }

    public void DeadGhoul() {
        if (isDead) return;
        Destroy(ghoulObject);
        GameManager.instance.IncrementGhoulCount(); //Call for GameManager
    }
}
