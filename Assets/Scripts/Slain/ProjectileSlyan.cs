using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSlyan : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 initialDirection;

    void Start()
    {
        ChangeColor();
    }

    void Update()
    {
        // Mover el proyectil en su dirección inicial
        float step = speed * Time.deltaTime;
        transform.position += initialDirection * step;
    }

    public void SetInitialDirection(Vector3 direction)
    {
        initialDirection = direction.normalized; // Normalizar la dirección
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HorusLife player = other.gameObject.GetComponent<HorusLife>();
            if (player != null)
            {
                player.HorusTakeDamage(1);
            }
            Destroy(gameObject); // Destruir el proyectil al colisionar
        }
    }

    void ChangeColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
