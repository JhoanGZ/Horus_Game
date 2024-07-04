using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo; //para posicion del player
    private Vector3 diferencia;
    void Awake()
    {
        diferencia = transform.position - objetivo.position; //posicion camara menos la del player
    }

    private void LateUpdate() //Metodo para actualizar en cada cuadro pero solo cuando hay cambios
    {
        transform.position = objetivo.position + diferencia; //Mueve la camara
    }


}