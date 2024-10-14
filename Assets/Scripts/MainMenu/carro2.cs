using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDerecho : MonoBehaviour
{
    public Transform ruedaDelanteraIzquierda;
    public Transform ruedaDelanteraDerecha;
    public Transform ruedaTraseraIzquierda;
    public Transform ruedaTraseraDerecha;

    public float velocidad = 10f;           // Velocidad de avance del auto
    public float distanciaRecorrido = 10f;  // Distancia que el auto recorre
    public float velocidadRotacionRuedas = 300f; // Velocidad de rotación de las ruedas
    public float tiempoEspera = 10f;        // Tiempo de espera antes de reiniciar el recorrido

    private Vector3 posicionInicial;
    private float distanciaRecorrida;

    void Start()
    {
        // Guarda la posición inicial del auto
        posicionInicial = transform.position;

        // Comienza el ciclo de recorrido
        StartCoroutine(CicloRecorrido());
    }

    private IEnumerator CicloRecorrido()
    {
        while (true) // Bucle infinito para repetir el recorrido
        {
            distanciaRecorrida = 0f;

            // Fase 1: Avanza en línea recta hasta recorrer la distancia especificada
            while (distanciaRecorrida < distanciaRecorrido)
            {
                MoverAuto();
                RotarRuedas();
                distanciaRecorrida += velocidad * Time.deltaTime;
                yield return null;
            }

            // Espera unos segundos antes de reiniciar el recorrido
            yield return new WaitForSeconds(tiempoEspera);

            // Regresa a la posición inicial
            transform.position = posicionInicial;

            // Espera unos segundos antes de comenzar otro recorrido
            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    // Método para mover el auto hacia adelante
    private void MoverAuto()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    // Método para hacer que las ruedas giren
    private void RotarRuedas()
    {
        // Ruedas delanteras
        ruedaDelanteraIzquierda.Rotate(Vector3.right * velocidadRotacionRuedas * Time.deltaTime);
        ruedaDelanteraDerecha.Rotate(Vector3.right * velocidadRotacionRuedas * Time.deltaTime);

        // Ruedas traseras
        ruedaTraseraIzquierda.Rotate(Vector3.right * velocidadRotacionRuedas * Time.deltaTime);
        ruedaTraseraDerecha.Rotate(Vector3.right * velocidadRotacionRuedas * Time.deltaTime);
    }
}

