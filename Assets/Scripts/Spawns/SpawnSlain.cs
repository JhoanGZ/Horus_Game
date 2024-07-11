using UnityEngine;

public class SpawnSlain : MonoBehaviour
{
    public GameObject bossPrefab;  // Prefab del jefe
    public Transform player;  // Referencia al jugador
    public float spawnRadius = 50f;  // Radio de spawn
    public AnimationClip spawnAnimation;  // Animación de spawn

    private bool hasSpawnedBoss = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !hasSpawnedBoss)
        {
            SpawnBoss();
            hasSpawnedBoss = true;
        }
    }

    public void SpawnBoss()
    {
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = player.position.y;  

        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        // First Animation
        Animator animator = boss.GetComponent<Animator>();
        if (animator != null && spawnAnimation != null)
        {
            animator.Play(spawnAnimation.name);
        }

        AudioSource audioSource = boss.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
