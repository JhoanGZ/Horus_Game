using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform horus;
    public float speed = 2f;
    // Start is called before the first frame update

    private void Start()
    {
        //Al ser creador proceduralmente estos deben buscar al objeto una vez inician
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            horus = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (horus != null)
        {
            Vector3 direction = horus.position - transform.position;
            direction.Normalize();


            // Mueve el enemigo hacia el jugador
            transform.position += direction * speed * Time.deltaTime;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Mirar a la izquierda
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Mirar a la derecha
            }
        }

      
    }
}
