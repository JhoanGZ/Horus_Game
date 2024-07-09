using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HorusLife player = other.gameObject.GetComponent<HorusLife>();
        if (player != null)
        {
            player.HorusTakeDamage(1);
        }
    }
}
