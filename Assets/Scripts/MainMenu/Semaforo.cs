using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour
{
    public Light redLight1, yellowLight1, greenLight1; // Primer conjunto
    public Light redLight2, yellowLight2, greenLight2; // Segundo conjunto
    public Light redLight3, yellowLight3, greenLight3; // Tercer conjunto

    private float tiempoVerde = 5f; // Tiempo que la luz verde estará encendida
    private float tiempoAmarillo = 2f; // Tiempo que la luz amarilla estará encendida
    private float tiempoRojo = 5f; // Tiempo que la luz roja estará encendida
    private float tiempoParpadeo = 0.5f; // Duración del parpadeo

    void Start()
    {
        // Apagar todas las luces al iniciar
        DesactivarLuz(redLight1, yellowLight1, greenLight1);
        DesactivarLuz(redLight2, yellowLight2, greenLight2);
        DesactivarLuz(redLight3, yellowLight3, greenLight3);

        // Inicia el ciclo del semáforo
        StartCoroutine(CicloSemaforo());
    }

    private IEnumerator CicloSemaforo()
    {
        while (true) // Bucle infinito para simular el ciclo del semáforo
        {
            // Luz verde en conjuntos 1 y 2, rojo en conjunto 3
            ActivarLuz(greenLight1, greenLight2);
            ActivarLuz(redLight3);
            DesactivarLuz(redLight1, redLight2); // Apagar el rojo en conjunto 1 y 2
            yield return StartCoroutine(ParpadearAntesDeCambiar(greenLight1, greenLight2, redLight3));
            yield return new WaitForSeconds(tiempoVerde);

            // Luz amarilla en conjuntos 1 y 2, rojo en conjunto 3
            ActivarLuz(yellowLight1, yellowLight2);
            DesactivarLuz(greenLight1, greenLight2); // Apagar el verde
            yield return StartCoroutine(ParpadearAntesDeCambiar(yellowLight1, yellowLight2));
            yield return new WaitForSeconds(tiempoAmarillo);
            DesactivarLuz(yellowLight1, yellowLight2);

            // Luz roja en conjuntos 1 y 2, verde en conjunto 3
            ActivarLuz(redLight1, redLight2);
            DesactivarLuz(redLight3); // Apagar el rojo en el conjunto 3
            ActivarLuz(greenLight3);
            yield return StartCoroutine(ParpadearAntesDeCambiar(redLight1, redLight2, greenLight3));
            yield return new WaitForSeconds(tiempoRojo);

            // Luz amarilla en conjunto 3, rojo en conjuntos 1 y 2
            ActivarLuz(yellowLight3);
            DesactivarLuz(greenLight3); // Apagar el verde en el conjunto 3
            ActivarLuz(redLight1, redLight2); // Mantener rojo en conjunto 1 y 2
            yield return StartCoroutine(ParpadearAntesDeCambiar(yellowLight3));
            yield return new WaitForSeconds(tiempoAmarillo);
            DesactivarLuz(yellowLight3);
        }
    }

    private IEnumerator ParpadearAntesDeCambiar(params Light[] luces)
    {
        // Parpadeo antes de cambiar de estado
        for (int i = 0; i < 3; i++) // Parpadea 3 veces
        {
            DesactivarLuz(luces);
            yield return new WaitForSeconds(tiempoParpadeo);
            ActivarLuz(luces);
            yield return new WaitForSeconds(tiempoParpadeo);
        }
    }

    private void ActivarLuz(params Light[] luces)
    {
        // Activa las luces especificadas sin desactivar otras
        foreach (var luz in luces)
        {
            if (luz != null)
                luz.intensity = 3f; // Enciende la luz con mayor intensidad
        }
    }

    private void DesactivarLuz(params Light[] luces)
    {
        foreach (var luz in luces)
        {
            if (luz != null)
                luz.intensity = 0f; // Apaga la luz
        }
    }
}


