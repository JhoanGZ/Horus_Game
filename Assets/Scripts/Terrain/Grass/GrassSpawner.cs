using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPrefab;
    public Transform player;
    public float spawnDistance = 4.0f;
    public int minGrassPerSpawn = 8;
    public int maxGrassPerSpawn = 18;
    public float minGrassDistance = 3.0f;
    public float fixedYPosition = 0.15f;
    public float spawnInterval = 2.0f;
    public float initialSpawnRadius = 10.0f; // Radio inicial para spawn de césped

    private List<Vector3> spawnedPositions = new List<Vector3>();
    private Vector3 lastSpawnPosition;

    void Start()
    {
        lastSpawnPosition = player.position;
        InitialSpawnGrass();
        StartCoroutine(CheckAndSpawnGrass());
    }

    void InitialSpawnGrass()
    {
        // Generar césped alrededor del jugador en un radio inicial
        int initialGrassToSpawn = UnityEngine.Random.Range(minGrassPerSpawn, maxGrassPerSpawn + 1);
        for (int i = 0; i < initialGrassToSpawn; i++)
        {
            Vector3 spawnPosition;
            bool positionFound = false;
            int attempts = 0;
            int maxSpawnAttempts = 10;

            while (!positionFound && attempts < maxSpawnAttempts)
            {
                float randomX = UnityEngine.Random.Range(-initialSpawnRadius, initialSpawnRadius);
                float randomZ = UnityEngine.Random.Range(-initialSpawnRadius, initialSpawnRadius);
                spawnPosition = new Vector3(player.position.x + randomX, fixedYPosition, player.position.z + randomZ);
                bool isTooClose = false;

                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minGrassDistance)
                    {
                        isTooClose = true;
                        break;
                    }
                }

                if (!isTooClose)
                {
                    positionFound = true;
                    spawnedPositions.Add(spawnPosition);

                    GameObject grass = Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = grass.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }

                attempts++;
            }

            //if (!positionFound)
            //{
            //    UnityEngine.Debug.LogWarning("Could not find a suitable position to spawn initial grass after " + maxSpawnAttempts + " attempts.");
            //}
        }
    }

    IEnumerator CheckAndSpawnGrass()
    {
        while (true)
        {
            if (Vector3.Distance(player.position, lastSpawnPosition) >= spawnDistance)
            {
                Vector3 moveDirection = (player.position - lastSpawnPosition).normalized;
                SpawnGrass(moveDirection);
                lastSpawnPosition = player.position;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnGrass(Vector3 direction)
    {
        int grassToSpawn = UnityEngine.Random.Range(minGrassPerSpawn, maxGrassPerSpawn + 1);
        for (int i = 0; i < grassToSpawn; i++)
        {
            Vector3 spawnPosition;
            bool positionFound = false;
            int attempts = 0;
            int maxSpawnAttempts = 10;

            while (!positionFound && attempts < maxSpawnAttempts)
            {
                float randomDistance = UnityEngine.Random.Range(0, spawnDistance);
                Vector3 offset = direction * randomDistance + new Vector3(UnityEngine.Random.Range(-spawnDistance, spawnDistance), 0, UnityEngine.Random.Range(-spawnDistance, spawnDistance));
                spawnPosition = player.position + offset;
                spawnPosition.y = fixedYPosition;
                bool isTooClose = false;

                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minGrassDistance)
                    {
                        isTooClose = true;
                        break;
                    }
                }

                if (!isTooClose)
                {
                    positionFound = true;
                    spawnedPositions.Add(spawnPosition);

                    GameObject grass = Instantiate(grassPrefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = grass.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }

                attempts++;
            }

            //if (!positionFound)
            //{
            //    UnityEngine.Debug.LogWarning("Could not find a suitable position to spawn grass after " + maxSpawnAttempts + " attempts.");
            //}
        }
    }
}
