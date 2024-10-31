using UnityEngine;

public class HintsController : MonoBehaviour
{
    // Referencias a los GameObjects que contienen los textos en el Canvas
    public GameObject textWASD;
    public GameObject textJUMP;
    public GameObject textSHOOT;

    // Se activa cuando el jugador entra en el collider
    private void OnTriggerEnter(Collider other)
    {
            // Identificar qué collider ha sido activado por su nombre
            if (other.CompareTag("DialogueWASD"))
            {
                textWASD.SetActive(true);   // Activar texto WASD
            }
            else if (other.CompareTag("DialogueJUMP"))
            {
                textJUMP.SetActive(true);   // Activar texto JUMP
            }
            else if (other.CompareTag("DialogueSHOOT"))
            {
                textSHOOT.SetActive(true);  // Activar texto SHOOT
            }
        
    }

    // Se desactiva cuando el jugador sale del collider
    private void OnTriggerExit(Collider other)
    {
       
            // Identificar qué collider ha sido desactivado por su nombre
            if (other.CompareTag("DialogueWASD"))
            {
                textWASD.SetActive(false);   // Desactivar texto WASD
            }
           else if (other.CompareTag("DialogueJUMP"))
            {
                textJUMP.SetActive(false);   // Desactivar texto JUMP
            }
                 else if (other.CompareTag("DialogueSHOOT"))
            {
                textSHOOT.SetActive(false);  // Desactivar texto SHOOT
            }
        
    }
}
