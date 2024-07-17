using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SlyanLife : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public UnityEvent<int> setHealth;
    public float invulnerabilityDuration = 1.5f;
    private bool live = true;
    private SpriteRenderer spriteRenderer;
    public float blinkDuration = 1.0f;
    public float blinkInterval = 0.1f;
    private bool isInvulnerable = false;

    private AudioSource audioSource;
    public AudioClip secondPhaseMusic;



    private SlainFollow slyanFollow;

    void Start()
    {
        currentHealth = maxHealth > 0 ? maxHealth : 10;
        setHealth.Invoke(currentHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtener la referencia al componente SlainFollow
        slyanFollow = GetComponent<SlainFollow>();
        if (slyanFollow == null)
        {
            Debug.LogError("SlainFollow component not found on the GameObject.");
        }

        audioSource = GameManager.instance.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
        }
    }

    public bool Damaged(int damageAmount)
    {
        if (isInvulnerable) return live;

        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        setHealth.Invoke(currentHealth);
        StartCoroutine(BlinkCoroutine());
        Debug.Log("HP SLYAN:: " + currentHealth);

        if (currentHealth <= maxHealth / 2)
        {
            PhaseChange();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.DeadSlain();
        }

        StartCoroutine(InvulnerabilityCoroutine());
        live = true;
        return live;
    }

    private IEnumerator BlinkCoroutine()
    {
        float elapsedTime = 0f;
        bool toggleColor = false;

        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = toggleColor ? Color.white : Color.red;
            toggleColor = !toggleColor;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.color = Color.white;
    }

    public void SlyanHeal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        setHealth.Invoke(currentHealth);
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private IEnumerator Dead()
    {
        // Opcional: Agregar lógica de animación de muerte aquí
        Destroy(gameObject);
        yield return new WaitForSeconds(1f);

        // Destruir todos los objetos con el tag "Slyan"
        GameObject[] slyans = GameObject.FindGameObjectsWithTag("Slyan");
        foreach (GameObject slyan in slyans)
        {
            Destroy(slyan);
        }

        SceneManager.LoadScene("Final");
    }

    private void PhaseChange()
    {
        if (slyanFollow != null)
        {
            slyanFollow.shouldFollowPlayer = false;
            slyanFollow.iPos = 1f;
            slyanFollow.animator.SetTrigger("ChangeAnimation");
            slyanFollow.secondPhase = true;

            if (audioSource != null && secondPhaseMusic != null)
            {
                audioSource.clip = secondPhaseMusic;
                audioSource.volume = 0.4f;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("slyanFollow reference is null.");
        }
    }
}
