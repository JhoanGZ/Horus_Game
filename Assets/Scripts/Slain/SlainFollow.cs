using UnityEngine;

public class SlainFollow : MonoBehaviour
{
    public Transform horus;
    public float moveSpeed = 3f; // Speed walk
    public float orbitSpeed = 1f; // Speed around
    public Animator animator;
    private bool shouldFollowPlayer = true;
    private float minDistance = 3f;
    private float maxDistance = 3.5f;

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
                transform.position = new Vector3(newPosition.x, 1f, newPosition.z); // Set Y = 1

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
                // Move around
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
