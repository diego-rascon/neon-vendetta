using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    public Transform ruedaDelanteraIzquierda;
    public Transform ruedaDelanteraDerecha;
    public Transform ruedaTraseraIzquierda;
    public Transform ruedaTraseraDerecha;

    public float velocidad = 10f;           // Velocidad de avance del auto
    public float distanciaRecta = 10f;      // Distancia que el auto recorre antes de girar
    public float distanciaPostGiro = 10f;   // Distancia que recorre después de girar
    public float anguloGiro = 30f;          // Ángulo de giro de las ruedas delanteras
    public float velocidadRotacionRuedas = 300f; // Velocidad de rotación de las ruedas
    public float tiempoGiro = 2f;           // Tiempo que tarda en completar el giro
    public float tiempoEspera = 10f;        // Tiempo de espera entre recorridos

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        // Guarda la posición y rotación inicial del auto
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;

        // Comienza el ciclo de recorrido
        StartCoroutine(CicloRecorrido());
    }

    private IEnumerator CicloRecorrido()
    {
        while (true) // Bucle infinito para repetir el recorrido
        {
            // Fase 1: Avanza en línea recta hasta recorrer la distancia especificada
            float distanciaRecorrida = 0f;
            while (distanciaRecorrida < distanciaRecta)
            {
                MoverAuto();
                RotarRuedas();
                distanciaRecorrida += velocidad * Time.deltaTime;
                yield return null;
            }

            // Fase 2: Comienza a girar cuando llega a la distancia recta
            yield return StartCoroutine(GirarAuto());

            // Fase 3: Sigue avanzando en línea recta después del giro hasta la distancia post giro
            distanciaRecorrida = 0f;
            while (distanciaRecorrida < distanciaPostGiro)
            {
                MoverAuto();
                RotarRuedas();
                distanciaRecorrida += velocidad * Time.deltaTime;
                yield return null;
            }

            // Espera 10 segundos antes de reiniciar el recorrido
            yield return new WaitForSeconds(tiempoEspera);

            // Regresa a la posición y rotación inicial
            transform.position = posicionInicial;
            transform.rotation = rotacionInicial;

            // Espera 10 segundos antes de comenzar otro recorrido
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

    // Corrutina para girar el auto a la derecha simulando un auto real
    private IEnumerator GirarAuto()
    {
        // Mueve las ruedas delanteras hacia la dirección del giro
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoGiro)
        {
            float anguloDeGiro = Mathf.Lerp(0f, anguloGiro, tiempoTranscurrido / tiempoGiro);
            RuedasGiran(anguloDeGiro);

            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);  // Sigue avanzando

            // Gira el auto hacia la derecha
            transform.Rotate(Vector3.up, (90f / tiempoGiro) * Time.deltaTime);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        // Regresa las ruedas delanteras a su posición inicial
        RuedasGiran(0f);
    }

    // Método para rotar las ruedas delanteras al girar
    private void RuedasGiran(float angulo)
    {
        ruedaDelanteraIzquierda.localRotation = Quaternion.Euler(0f, angulo, 0f);
        ruedaDelanteraDerecha.localRotation = Quaternion.Euler(0f, angulo, 0f);
    }
}





  