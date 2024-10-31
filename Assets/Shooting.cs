using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public AudioSource shootAudioSource; // Asigna tu AudioSource del disparo aquí
    public Transform shootPoint; // Punto desde donde se dispara

    void Update()
    {
        // Comprobar si se presiona el botón izquierdo del ratón (clic)
        if (Input.GetMouseButtonDown(0)) // 0 corresponde al clic izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Reproducir sonido de disparo
        shootAudioSource.Play();


        // Aquí puedes agregar más lógica para el disparo, como la dirección de la bala y la física
    }
}