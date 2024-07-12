using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static int skillLevel = 1; // Nivel del proyectil
    public static int baseDamage = 2; // Daño base
    public static float baseSpeed = 6f; // Velocidad del proyectil
    public float lifeTime = 7f; // Tiempo de vida del proyectil
    private static int killCount = 0;
    private static int skillExperience = 0;
    private static int skillExperiencePerKill = 10;
    private static int skillExperienceRequired = 100;

    private static Transform target;
    private static float finalDamage;
    private static float finalSpeed;
    private Transform player;

    private void Awake()
    {
        UpdateProjectileStats();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = FindClosestEnemy();
        Destroy(gameObject, lifeTime); // Destruye el proyectil después de un tiempo
    }

    private void Update()
    {
        target = FindClosestEnemy();
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.position += direction * finalSpeed * Time.deltaTime;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private Transform FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        SlyanLife[] slyans = FindObjectsOfType<SlyanLife>();
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (SlyanLife slyan in slyans)
        {
            if (slyan.CompareTag("Slyan"))
            {
                // Priorizar Slyan independientemente de la distancia
                return slyan.transform;
            }
        }

        // Si no hay Slyan, buscar el enemigo más cercano
        foreach (Enemy enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void OnTriggerEnter(Collider other)
{
    Enemy enemy = other.GetComponent<Enemy>();
    SlyanLife slyan = other.GetComponent<SlyanLife>();
    
    if (enemy != null || slyan != null)
    {
        bool targetAlive = true;

        // Verifica si el objeto tiene el tag Slyan
        if (other.CompareTag("Slyan"))
        {
            targetAlive = slyan.Damaged(1); // Daño fijo para Slyan
        }
        else if (enemy != null)
        {
            targetAlive = enemy.Damaged((int)finalDamage);
        }

        Destroy(gameObject); // Destruye el proyectil al impactar

        if (!targetAlive)
        {
            HandleKillCount();
        }
    }
}


    private void HandleKillCount()
    {
        killCount++;
        GameManager.instance.IncrementGhoulCount();
        skillExperience += skillExperiencePerKill;
        if (skillExperience >= skillExperienceRequired)
        {
            skillLevel++;
            skillExperience = 0;
            UpdateProjectileStats();
        }
    }

    private static void UpdateProjectileStats()
    {
        float damageGrowFactor = skillLevel / 20f;
        float damageMultiplierPerLevel = 1 + damageGrowFactor;
        finalDamage = baseDamage * damageMultiplierPerLevel;

        float speedGrowFactor = skillLevel / 30f;
        float speedMultiplierPerLevel = 1f + speedGrowFactor;
        finalSpeed = baseSpeed * speedMultiplierPerLevel;
    }
}
