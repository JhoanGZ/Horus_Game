using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalProjectile : MonoBehaviour
{
    public int baseDamage = 2; // Daño base
    public float damageRadius = 0.5f; // Radio de daño del proyectil
    public float blinkInterval = 0.5f; // Intervalo de parpadeo en segundos
    public float rotationSpeed = 100f; // Velocidad de rotación en grados por segundo

    private Renderer projectileRenderer;
    private Color[] colors = { Color.black, Color.blue, Color.red };
    private OrbitalProjectiles orbitalProjectilesScript;

    private void Awake()
    {
        projectileRenderer = GetComponent<Renderer>();
        StartCoroutine(BlinkColors());
        orbitalProjectilesScript = FindObjectOfType<OrbitalProjectiles>(); // Buscar el script controlador de proyectiles orbitales
    }

    private void Update()
    {
        RotateProjectile();
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
                targetAlive = enemy.Damaged(baseDamage);
            }

            if (!targetAlive)
            {
                orbitalProjectilesScript.AddExperience(orbitalProjectilesScript.skillExperiencePerKill);
            }
        }
    }

    private IEnumerator BlinkColors()
    {
        int colorIndex = 0;
        while (true)
        {
            projectileRenderer.material.color = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void RotateProjectile()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
