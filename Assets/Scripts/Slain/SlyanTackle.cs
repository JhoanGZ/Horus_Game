using System.Collections;
using UnityEngine;

public class SlyanTackle : MonoBehaviour
{
    public float tackleSpeed = 10f;
    public float tackleDuration = 0.5f;
    public int maxPivots = 3;
    public float pivotAngle = 45f;
    public float pivotDuration = 0.2f;
    
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ExecuteTackle()
    {
        StartCoroutine(TackleCoroutine());
    }

    private IEnumerator TackleCoroutine()
    {
        int pivotCount = Random.Range(1, maxPivots + 1);

        // Realizar pivotes
        for (int i = 0; i < pivotCount; i++)
        {
            float angle = Random.Range(-pivotAngle, pivotAngle);
            Vector3 pivotDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            transform.position += pivotDirection * tackleSpeed * pivotDuration;
            yield return new WaitForSeconds(pivotDuration);
        }

        // Dirigir hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Realizar la tacleada
        float tackleTime = 0f;
        while (tackleTime < tackleDuration)
        {
            transform.position += direction * tackleSpeed * Time.deltaTime;
            tackleTime += Time.deltaTime;
            yield return null;
        }
    }
}
