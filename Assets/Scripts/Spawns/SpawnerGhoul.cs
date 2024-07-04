using UnityEngine;

public class SpawnerGhoul : MonoBehaviour
{
    public GameObject ghoulPrefab;  // Prefab del enemigo
    public Transform player;  // Referencia al jugador
    public float spawnRadius = 50f;  // Radio de spawn
    public float spawnInterval = 5f;  // Intervalo de tiempo entre spawns

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = player.position.y;  // Mantener el mismo nivel del jugador
        Instantiate(ghoulPrefab, spawnPosition, Quaternion.identity);
    }
}