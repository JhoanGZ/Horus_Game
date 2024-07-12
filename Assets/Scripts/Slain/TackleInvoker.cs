using System.Collections;
using UnityEngine;

public class TackleInvoker : MonoBehaviour
{
    public float tackleCooldown = 5f;
    private float lastTackleTime;
    private SlyanTackle slyanTackle;

    void Start()
    {
        slyanTackle = GetComponent<SlyanTackle>();
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
            yield return new WaitForSeconds(Random.Range(5f, 8f)); // Espera entre 3 y 8 segundos
            if (SlainFollow.secondPhase == false && Time.time >= lastTackleTime + tackleCooldown)
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
