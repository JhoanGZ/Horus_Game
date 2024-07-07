using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class LifeUI : MonoBehaviour
{
    public List<Image> listHearths;
    public GameObject hearthPrefab;
    public HorusLife horusLife;
    public int currentIndex;

    public static object Instance { get; internal set; }

    private void Awake()
    {
        horusLife.setHealth.AddListener(ChangeHearths);
    }

    private void ChangeHearths(int currentHealth) //Call for functions
    {
        if (!listHearths.Any())
        {
            CreateHearth(currentHealth);
        }
        else
        {
            UpdateHealth(currentHealth);
        }
    }



    private void CreateHearth(int maxHealth) //Create list of life
    {
        for(int i = 0; i< maxHealth; i++)
        {
            GameObject hearth = Instantiate(hearthPrefab, transform);
            listHearths.Add(hearth.GetComponent<Image>());
        }
        currentIndex = maxHealth - 1;
    }

    private void UpdateHealth(int currentHealth) //Update actual health
    {
        if(currentHealth <= currentIndex)
        {
            RemoveHearth(currentHealth);
        }else
        {
            AddHearth(currentHealth);
        }
    }

    private void RemoveHearth(int currentHealth) //Remove hearth when horus take damage
    {
        for (int i = currentIndex; i >= currentHealth; i--)
        {
            listHearths[i].gameObject.SetActive(false); 
            currentIndex = i - 1;
        }
    }

    private void AddHearth(int currentHealth) //Add heart when healing.
    {
        for (int i = currentIndex + 1; i < currentHealth; i++)
        {
            listHearths[i].gameObject.SetActive(true); 
            currentIndex = i;
        }
    }


}
