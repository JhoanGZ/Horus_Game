using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Horus : MonoBehaviour
{
    // Velocidad de movimiento
    public float speed = 4.0f;
    private Animator animator;
    private float TimeWalking;
    public float dashDuration = 2f;
    private HorusLife horusLife;
    private bool isInvulnerable = false; //Verification for invulnerability
    public GameObject cooldownBar;


    void Start()
    {
        // Inicializar el componente Animator
        animator = GetComponent<Animator>();
        TimeWalking = 0f; //Controlador para cambio de animacion caminar
        horusLife = GetComponent<HorusLife>();
        cooldownBar.gameObject.SetActive(false);

    }

    // Update se llama una vez por frame
    void Update()
    {
        // Movimiento
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Crea un vector de movimiento basado en la entrada del usuario
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Mueve el objeto según el vector de movimiento y la velocidad
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Controla el sprite izquierda o derecha
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha
        }

        // Detección de movimiento
        bool isWalking = movement.magnitude > 0;

        if (isWalking)
        {
            animator.SetBool("IsWalking", true);
            TimeWalking += Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsWalking", false);
            TimeWalking = 0f;
        }

        animator.SetFloat("TimeWalking", TimeWalking);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
}

    IEnumerator Dash() 
    {
        animator.SetBool("IsDashing", true);
        speed = 7f;
        SetInvulnerable(true);//set invulnerability
        cooldownBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(dashDuration);
        SetInvulnerable(false);//Disable invulnerability
        animator.SetBool("IsDashing", false);
        speed = 4f;
        cooldownBar.gameObject.SetActive(false);

    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public void SetInvulnerable(bool state)
    {
        isInvulnerable = state;
    }
}
