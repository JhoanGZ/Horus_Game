using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static int skillLevel = 1; // Nivel del proyectil
    public static int baseDamage = 2; // Daño base
    public static float baseSpeed = 6f; // Velocidad del proyectil
    public float lifeTime = 7f; // Tiempo de vida del proyectil antes de destruirse
    private static int killCount = 0;
    private static int skillExperience = 0;
    private static int skillExperiencePerKill = 10;
    private static int skillExperienceRequired = 100;

    private Transform target;
    private static float finalDamage;
    private static float finalSpeed;

    private void Awake()
    {
        UpdateProjectileStats();
    }

    private void Start()
    {
        target = FindClosestEnemy();
        if (target == null)
        {
            Debug.LogWarning("No se encontró ningún enemigo para perseguir.");
            Destroy(gameObject, 0); // Destruye el proyectil después de un tiempo si no hay enemigo
        }
        else
        {
            Destroy(gameObject, lifeTime); // Destruye el proyectil después de un tiempo incluso si hay enemigo
        }
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // Move the projectile towards the enemy
            transform.position += direction * finalSpeed * Time.deltaTime;
        }
    }

    private Transform FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Método que se llama cuando el proyectil colisiona con otro objeto.
        // Si el objeto es un enemigo, se le resta vida y se destruye el
        // proyectil.
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            //Debug.Log("Hit enemy: " + other.gameObject.name);
            bool kill = enemy.Damaged((int)finalDamage); // Assuming Damaged method returns a bool for kill
            Destroy(gameObject); // Destruye el proyectil al impactar con el enemigo

            if (!kill)
            {
                killCount++;
                GameManager.instance.IncrementGhoulCount(); //Add count for canvas
                Debug.Log("Skill kill count = " + killCount);
                skillExperience += skillExperiencePerKill;
                if (skillExperience >= skillExperienceRequired)
                {
                    skillLevel++;
                    skillExperience = 0;
                    Debug.Log("BasicAttack LevelUP = LV." + skillLevel);
                    UpdateProjectileStats();
                }
            }
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

        Debug.Log("Updated Stats: Damage = " + finalDamage + ", Speed = " + finalSpeed);
    }
}
