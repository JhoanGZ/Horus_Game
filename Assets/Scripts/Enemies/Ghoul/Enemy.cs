using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float gameTime = 0f; // Tiempo de la partida en segundos
    public float baseHitPoints = 3f; // Vida base
    public float lifeMultiplier = 1f; // Multiplicador de vida inicial
    public float growthFactor = 0.05f; // Factor de crecimiento inicial
    public float growthFactorIncrement = 0.01f; // Incremento del factor de crecimiento
    public bool alive = true;
    public int damage = 1;
    private float hitPoints;

    private static float nextGrowthTime = 10f; // Tiempo para el próximo crecimiento de vida
    private static float nextGrowthFactorTime = 30f; // Tiempo para el próximo incremento del factor de crecimiento

    // Start is called before the first frame update
    void Start()
    {
        UpdateHitPoints();
    }

    // Update is called once per frame
    void Update()
    {
        // Incrementar el tiempo de la partida
        gameTime += Time.deltaTime;

        // Incrementar el multiplicador de vida cada 10 segundos
        if (gameTime >= nextGrowthTime)
        {
            lifeMultiplier *= (1 + growthFactor);
            nextGrowthTime += 10f;
            UpdateHitPoints(); // Actualizar puntos de vida tras el crecimiento
        }

        // Incrementar el factor de crecimiento cada 30 segundos
        if (gameTime >= nextGrowthFactorTime)
        {
            growthFactor += growthFactorIncrement;
            nextGrowthFactorTime += 30f;
        }
    }

    // Método para manejar el daño al enemigo
    public bool Damaged(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0 && alive)
        {
            alive = false;
            Destroy(gameObject);
        }
        return alive;
    }

    private void UpdateHitPoints()
    {
        hitPoints = baseHitPoints * lifeMultiplier;
        //Debug.Log("Updated HitPoints: " + hitPoints);
    }
}
