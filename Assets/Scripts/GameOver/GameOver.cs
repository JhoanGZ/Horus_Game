using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;

    private Animator animator;

    private void Start()
    {
        // Object animator
        animator = GetComponent<Animator>();

        // Disable canvas
        if (gameOver != null)
        {
            gameOver.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for loo
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            // Activate canvas
            if (gameOver != null)
            {
                gameOver.SetActive(true);
            }
        }
    }
}
