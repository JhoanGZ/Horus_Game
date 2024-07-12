using System.Collections;
using UnityEngine;

public class TackleInvoker : MonoBehaviour
{
    public float tackleCooldown = 5f;
    private float lastTackleTime;
    private SlyanTackle slyanTackle;
    private SlainFollow slyanFollow; // Referencia al componente SlainFollow

    void Start()
    {
        slyanTackle = GetComponent<SlyanTackle>();
        slyanFollow = GetComponent<SlainFollow>(); // Obtener la referencia al componente SlainFollow

        if (slyanFollow == null)
        {
            Debug.LogError("SlainFollow component not found on the GameObject.");
        }

        StartCoroutine(AutoInvokeTackle());
    }

    void Update()
    {
        // Invocación manual con la tecla "T"
        if (Input.GetKeyDown(KeyCode.T) && Time.time >= lastTackleTime + tackleCooldown)
        {
            ExecuteTackle();
        }
    }

    private IEnumerator AutoInvokeTackle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 8f)); // Espera entre 5 y 8 segundos
            if (slyanFollow != null && !slyanFollow.secondPhase && Time.time >= lastTackleTime + tackleCooldown)
            {
                ExecuteTackle();
            }
        }
    }

    private void ExecuteTackle()
    {
        slyanTackle.ExecuteTackle();
        lastTackleTime = Time.time;
    }
}
