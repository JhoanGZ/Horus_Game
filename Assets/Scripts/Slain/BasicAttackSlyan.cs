using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackSlyan : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    void Update()
    {
        if (SlainFollow.secondPhase && Time.time >= lastAttackTime + attackCooldown)
        {
            int pattern = Random.Range(0, 12);
            switch (pattern)
            {
                case 0: FireCirclePattern(); break;
                case 1: FireStarPattern(); break;
                case 2: FireSpiralPattern(); break;
                case 3: FireLinePattern(); break;
                case 4: FireVPattern(); break;
                case 5: FireCrossPattern(); break;
                case 6: FireWavePattern(); break;
                case 7: FireRandomPattern(); break;
                case 8: FireSpiralInwardsPattern(); break;
                case 9: FireConvergingLinesPattern(); break;
                case 10: FireCrescentMoonPattern(); break;
                case 11: FireSwirlingPattern(); break;
            }
            lastAttackTime = Time.time;
        }
    }

    void FireCirclePattern()
    {
        int projectilesCount = 20;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * (360f / projectilesCount);
            FireProjectile(angle);
        }
    }

    void FireStarPattern()
    {
        int projectilesCount = 10;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * (360f / projectilesCount) + 18;
            FireProjectile(angle);
        }
    }

    void FireSpiralPattern()
    {
        int projectilesCount = 10;
        float angleStep = 10;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * angleStep;
            FireProjectile(angle);
        }
    }

    void FireLinePattern()
    {
        int projectilesCount = 5;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = 0;
            FireProjectile(angle);
        }
    }

    void FireVPattern()
    {
        FireProjectile(15);
        FireProjectile(-15);
    }

    void FireCrossPattern()
    {
        FireProjectile(0);
        FireProjectile(90);
        FireProjectile(180);
        FireProjectile(270);
    }

    void FireWavePattern()
    {
        int projectilesCount = 10;
        float angleStep = 15;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = Mathf.Sin(i * angleStep) * 45;
            FireProjectile(angle);
        }
    }

    void FireRandomPattern()
    {
        int projectilesCount = 8;
        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = Random.Range(0, 360f);
            FireProjectile(angle);
        }
    }

    void FireSpiralInwardsPattern()
    {
        int projectilesCount = 12;
        float angleStep = 30;
        float radius = 5f;

        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 position = transform.position + direction * radius;
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

            ProjectileSlyan projectileScript = projectile.GetComponent<ProjectileSlyan>();
            if (projectileScript != null)
            {
                projectileScript.SetInitialDirection(direction);
            }
        }
    }

    void FireConvergingLinesPattern()
    {
        int projectilesCount = 8;
        float angleStep = 360f / projectilesCount;

        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * angleStep;
            FireProjectile(angle);
            FireProjectile(angle + 180);
        }
    }

    void FireCrescentMoonPattern()
    {
        int projectilesCount = 10;
        float angleStep = 36;
        float offset = 15f;

        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * angleStep;
            FireProjectile(angle + offset);
            FireProjectile(angle - offset);
        }
    }

    void FireSwirlingPattern()
    {
        int projectilesCount = 16;
        float angleStep = 360f / projectilesCount;

        for (int i = 0; i < projectilesCount; i++)
        {
            float angle = i * angleStep + (Time.time * 50);
            FireProjectile(angle);
        }
    }

    void FireProjectile(float angle)
    {
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        Vector3 slyanPosition = transform.position; // Obtener la posición de Slyan
        Vector3 spawnPosition = new Vector3(slyanPosition.x, 0.3f, slyanPosition.z); // Establecer Y en 0.3

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        ProjectileSlyan projectileScript = projectile.GetComponent<ProjectileSlyan>();
        if (projectileScript != null)
        {
            projectileScript.SetInitialDirection(direction);
        }
    }
}
