using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidoPasos : MonoBehaviour
{

    public AudioSource footstepAudioSource;
    public CharacterController characterController; // O el componente que maneje el movimiento
    public float walkingThreshold = 0.1f; // Umbral para considerar que el personaje está caminando

    void Update()
    {
        // Obtiene la velocidad del personaje
        if (characterController.velocity.magnitude > walkingThreshold && !footstepAudioSource.isPlaying)
        {
            // Si el personaje se está moviendo y el audio no está sonando, reproducir el sonido
            footstepAudioSource.Play();
        }
        else if (characterController.velocity.magnitude <= walkingThreshold && footstepAudioSource.isPlaying)
        {
            // Si el personaje no se está moviendo, detener el sonido
            footstepAudioSource.Stop();
        }
    }
}
