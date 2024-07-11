using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree02Spawner : MonoBehaviour
{
    public GameObject tree2Prefab;
    public Transform player;
    public float spawnDistance = 5.0f;
    public int minTreesPerSpawn = 2;
    public int maxTreesPerSpawn = 3;
    public float minTreeDistance = 2.0f; // Minimum distance between trees
    public float fixedYPosition = 0.95f; // Fixed Y position for trees

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
            // Spawn trees and update the last spawn position
            SpawnTrees();
            lastSpawnPosition = player.position;
        }
    }

    void SpawnTrees()
    {
        int treesToSpawn = UnityEngine.Random.Range(minTreesPerSpawn, maxTreesPerSpawn + 1);
        for (int i = 0; i < treesToSpawn; i++)
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

                // Check the minimum distance with other spawned tree positions
                bool tooClose = false;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minTreeDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                // If a valid position is found and not too close to the player, spawn the tree
                if (!tooClose && Vector3.Distance(spawnPosition, player.position) > minTreeDistance)
                {
                    positionFound = true;
                    spawnedPositions.Add(spawnPosition);

                    // Instantiate the tree and set its Rigidbody to be kinematic
                    GameObject tree = Instantiate(tree2Prefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = tree.GetComponent<Rigidbody>();
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
            //    UnityEngine.Debug.LogWarning("Failed to find a valid spawn position for a tree after multiple attempts.");
            //}
        }
    }
}
