using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonesSound : MonoBehaviour, IPointerEnterHandler
{
       public AudioSource hoverSound;

    // Se activa al hacer hover sobre el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            Debug.Log("hover");
            hoverSound.Play();
        }
    }
}
