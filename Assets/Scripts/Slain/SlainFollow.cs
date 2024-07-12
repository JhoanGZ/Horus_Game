using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SlainFollow : MonoBehaviour
{
    public Transform horus;
    public float moveSpeed = 3f; // Speed walk
    public float orbitSpeed = 0.5f; // Speed around
    public Animator animator;
    private bool shouldFollowPlayer = true;
    public static bool secondPhase = false;
    private float minDistance = 5f;
    private float maxDistance = 7f;
    private float iPos = 0.5f;
    public AudioClip secondPhaseAudio;

    void Start()
    {
        // Follow player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            horus = player.transform;
        }

        // Get animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (horus != null)
        {
            if (shouldFollowPlayer)
            {
                Vector3 direction = horus.position - transform.position;
                direction.y = 0f; // block y
                direction.Normalize();
                // Follow player
                Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(newPosition.x, iPos, newPosition.z); // Set Y = 0.48

                // Set scale for direction
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // Look left
                }
                else if (direction.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1); // Look right
                }
            }
            else
            {

                // Comportamiento 2da Fase
                if (!GetComponent<AudioSource>().isPlaying && secondPhaseAudio != null)
                {
                    GetComponent<AudioSource>().clip = secondPhaseAudio;
                    GetComponent<AudioSource>().Play();
                }
                secondPhase = true;
                float angle = Time.time * orbitSpeed; // speed around
                float distance = Mathf.Lerp(minDistance, maxDistance, Mathf.PingPong(angle, 1f));
                Vector3 circlePosition = horus.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
                transform.position = new Vector3(circlePosition.x, 1f, circlePosition.z); // Sey y
            }
        }

        // Change to animation 2
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeAnimation();
            iPos = 1f;
            shouldFollowPlayer = false; // stop follow
        }
    }

    void ChangeAnimation()
    {
        // Play trigger
        if (animator != null)
        {
            animator.SetTrigger("ChangeAnimation");
        }
    }
}
