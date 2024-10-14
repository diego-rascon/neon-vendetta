using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampara : MonoBehaviour
{
    private Light spotlight;
    public GameObject lightShape; // Arrastra tu objeto LightShape aquí en el Inspector
    private float tiempoParpadeo;

    void Start()
    {
        spotlight = GetComponent<Light>();
        tiempoParpadeo = Random.Range(0.5f, 1.5f);
        InvokeRepeating(nameof(Parpadear), 0f, tiempoParpadeo);
    }

    void Parpadear()
    {
        StartCoroutine(EfectoParpadeo());

        // Cambia el tiempo de parpadeo para el próximo
        tiempoParpadeo = Random.Range(0.5f, 1.5f);
        CancelInvoke(nameof(Parpadear));
        InvokeRepeating(nameof(Parpadear), tiempoParpadeo, tiempoParpadeo);
    }

    private IEnumerator EfectoParpadeo()
    {
        // Apaga el spotlight
        spotlight.intensity = 0f;
        // Desaparece el LightShape
        if (lightShape != null)
            lightShape.SetActive(false);

        // Espera un tiempo aleatorio entre 2 y 4 segundos
        float tiempoEspera = Random.Range(2f, 4f);
        yield return new WaitForSeconds(tiempoEspera);

        // Enciende el spotlight de nuevo
        spotlight.intensity = 1f; // Ajusta a la intensidad original
        // Vuelve a aparecer el LightShape
        if (lightShape != null)
            lightShape.SetActive(true);
    }
}

