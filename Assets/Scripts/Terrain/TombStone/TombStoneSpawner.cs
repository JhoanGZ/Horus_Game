using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStoneSpawner : MonoBehaviour
{
    public GameObject TombStonePrefab;
    public Transform player;
    public float spawnDistance = 5.0f;
    public int minTombStonePerSpawn = 2;
    public int maxTombStonePerSpawn = 3;
    public float minTombStoneDistance = 2.0f; // Minimum distance between TombStones
    public float fixedYPosition = 0.13f; // Fixed Y position for TombStones

    private List<Vector3> spawnedPositions = new List<Vector3>();
    private Vector3 lastSpawnPosition;

    void Start()
    {
        // Initialize last spawn position with the player's starting position
        lastSpawnPosition = player.position;
    }

    void Update()
    {
        // Check if the player has moved a certain distance from the last spawn position
        if (Vector3.Distance(player.position, lastSpawnPosition) >= spawnDistance)
        {
            SpawnTombStones();
            lastSpawnPosition = player.position;
        }
    }

    void SpawnTombStones()
    {
        int TombStonesToSpawn = UnityEngine.Random.Range(minTombStonePerSpawn, maxTombStonePerSpawn + 1);
        for (int i = 0; i < TombStonesToSpawn; i++)
        {
            Vector3 spawnPosition;
            bool positionFound = false;
            int attempts = 0;
            int maxSpawnAttempts = 10;

            // Attempt to find a valid spawn position
            while (!positionFound && attempts < maxSpawnAttempts)
            {
                float randomX = UnityEngine.Random.Range(-spawnDistance, spawnDistance);
                float randomZ = UnityEngine.Random.Range(-spawnDistance, spawnDistance);
                spawnPosition = new Vector3(player.position.x + randomX, fixedYPosition, player.position.z + randomZ);

                // Check the minimum distance with other spawned tombstone positions
                bool tooClose = false;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minTombStoneDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                // If a valid position is found and not too close to the player, spawn the tombstone
                if (!tooClose && Vector3.Distance(spawnPosition, player.position) > minTombStoneDistance)
                {
                    positionFound = true;
                    spawnedPositions.Add(spawnPosition);

                    // Instantiate the tombstone and set its Rigidbody to be kinematic
                    GameObject tombStone = Instantiate(TombStonePrefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = tombStone.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                }
                attempts++;
            }

            // Log a warning if a valid position wasn't found after multiple attempts
            //if (!positionFound)
            //{
            //    UnityEngine.Debug.LogWarning("Failed to find a valid spawn position for a tombstone after multiple attempts.");
            //}
        }
    }
}
