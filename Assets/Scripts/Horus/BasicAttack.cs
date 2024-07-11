using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se lanza el proyectil

    [Header("Animation")]
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Debug.Log("Instanciando proyectil desde prefab: " + projectilePrefab.name);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                //Debug.Log("Rigidbody encontrado en el proyectil.");
                rb.useGravity = false; // Desactiva la gravedad
                rb.velocity = firePoint.forward * 0.1f; // Lanza el proyectil hacia adelante inicialmente
            }
            else
            {
                //Debug.LogWarning("El componente 'Rigidbody' no se encontró en el proyectil.");
            }
        }
        else
        {
            //Debug.LogWarning("El prefab del proyectil o el punto de disparo no están asignados.");
        }
    }
}
