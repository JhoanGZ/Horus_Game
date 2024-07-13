using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalProjectiles : MonoBehaviour
{
    public GameObject orbitalProjectilePrefab; // Prefab del proyectil orbital
    public Transform player; // Transform del jugador
    public int initialNumberOfProjectiles = 2; // Número inicial de proyectiles orbitales
    public float orbitRadius = 1.5f; // Radio de la órbita
    public float orbitSpeed = 50f; // Velocidad de la órbita

    public int skillLevel = 1; // Nivel de habilidad
    public int skillExperience = 0; // Experiencia de habilidad
    public int skillExperiencePerKill = 10; // Experiencia ganada por cada enemigo eliminado
    public int skillExperienceRequired = 100; // Experiencia necesaria para subir de nivel

    private List<GameObject> orbitalProjectiles; // Lista de proyectiles orbitales

    void Start()
    {
        orbitalProjectiles = new List<GameObject>();
        CreateInitialProjectiles();
    }

    void Update()
    {
        UpdateProjectilesPosition();
    }

    private void CreateInitialProjectiles()
    {
        for (int i = 0; i < initialNumberOfProjectiles; i++)
        {
            CreateProjectile();
        }
    }

    private void CreateProjectile()
    {
        GameObject orbitalProjectile = Instantiate(orbitalProjectilePrefab, player.position, Quaternion.identity);
        orbitalProjectiles.Add(orbitalProjectile);
    }

    private void UpdateProjectilesPosition()
    {
        for (int i = 0; i < orbitalProjectiles.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / orbitalProjectiles.Count + Time.time * orbitSpeed;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * orbitRadius;
            orbitalProjectiles[i].transform.position = player.position + offset;
        }
    }

    public void AddExperience(int amount)
    {
        skillExperience += amount;
        if (skillExperience >= skillExperienceRequired)
        {
            skillExperience -= skillExperienceRequired;
            skillLevel++;
            skillExperienceRequired += 50; // Aumentar el requerimiento de experiencia para el próximo nivel
            UpdateProjectiles();
        }
    }

    private void UpdateProjectiles()
    {
        // Lógica para añadir un nuevo proyectil cada 5 niveles
        if (skillLevel % 5 == 0)
        {
            CreateProjectile();
        }
    }
}
