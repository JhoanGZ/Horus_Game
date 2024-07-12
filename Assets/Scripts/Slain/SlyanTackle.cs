using System.Collections;
using UnityEngine;

public class SlyanTackle : MonoBehaviour
{
    public float tackleSpeed = 10f;
    public float tackleDuration = 0.5f;
    public int maxPivots = 5; // Aumentar el número máximo de pivotes
    public float pivotDistance = 2f; // Distancia para el pivote
    public float pivotDuration = 0.2f;
    public float knockbackForce = 5f; // Fuerza de knockback
    public float blinkDuration = 0.1f; // Duración del parpadeo
    public Color blinkColor = new Color(0.5f, 0, 0.5f, 0.5f); // Color morado

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false; // Desactivar el TrailRenderer al inicio
        }
    }

    public void ExecuteTackle()
    {
        StartCoroutine(TackleCoroutine());
    }

    private IEnumerator TackleCoroutine()
    {
        int pivotCount = Random.Range(1, maxPivots + 1);
        Color originalColor = spriteRenderer.color;

        if (trailRenderer != null)
        {
            trailRenderer.enabled = true; // Activar el TrailRenderer al iniciar la tacleada
        }

        // Realizar pivotes cerca del jugador
        for (int i = 0; i < pivotCount; i++)
        {
            Vector3 pivotDirection = (i % 2 == 0) ? Vector3.right : Vector3.left;
            Vector3 pivotPosition = player.position + pivotDirection * pivotDistance;

            transform.position = Vector3.MoveTowards(transform.position, pivotPosition, tackleSpeed * pivotDuration);

            // Parpadeo
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(pivotDuration);
        }

        // Dirigir hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Realizar la tacleada
        float tackleTime = 0f;
        while (tackleTime < tackleDuration)
        {
            transform.position += direction * tackleSpeed * Time.deltaTime;
            tackleTime += Time.deltaTime;
            yield return null;
        }

        // Aplicar knockback al jugador al colisionar
        if (Vector3.Distance(transform.position, player.position) < 1.5f)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 knockbackDirection = (player.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }

        // Resetear el color del sprite al final
        spriteRenderer.color = originalColor;

        if (trailRenderer != null)
        {
            yield return new WaitForSeconds(trailRenderer.time); // Esperar hasta que el rastro desaparezca
            trailRenderer.enabled = false; // Desactivar el TrailRenderer al final
        }
    }
}
