using UnityEngine;

public class Horus : MonoBehaviour
{
    // Velocidad de movimiento
    public float speed = 5.0f;
    private Animator animator;
    private float TimeWalking;

    void Start()
    {
        // Inicializar el componente Animator
        animator = GetComponent<Animator>();
        TimeWalking = 0f; //Controlador para cambio de animacion caminar
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
    }

    
}
