using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BotonesInteraccion : MonoBehaviour
{
    public TextMeshPro boton1;
    public TextMeshPro boton2;
    public TextMeshPro botonSalir;
    public TextMeshPro botonVolver;
    public AudioSource hoverSound;
    public Camera camara1;
    public Camera camara2;
    public float duracionMovimiento = 1.5f;

    public float handHeldIntensity = 0.3f;
    public float handHeldFrequency = 3f;

    private bool enTransicion = false;
    private Vector3 posicionInicialCam1;
    private Quaternion rotacionInicialCam1;

    // New variables to store the base position and rotation
    private Vector3 basePosition;
    private Quaternion baseRotation;

    void Start()
    {
        posicionInicialCam1 = camara1.transform.position;
        rotacionInicialCam1 = camara1.transform.rotation;

        // Initialize base position and rotation
        basePosition = camara1.transform.position;
        baseRotation = camara1.transform.rotation;

        boton1.gameObject.AddComponent<MouseEvents>().SetFunction(PlayButton, hoverSound);
        boton2.gameObject.AddComponent<MouseEvents>().SetFunction(ControlsButton, hoverSound);
        botonSalir.gameObject.AddComponent<MouseEvents>().SetFunction(QuitButton, hoverSound);
        botonVolver.gameObject.AddComponent<MouseEvents>().SetFunction(BackButton, hoverSound);

        StartCoroutine(HandheldEffect());
    }

    void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }

    void ControlsButton()
    {
        if (!enTransicion)
        {
            StartCoroutine(InterpolarCamara(camara1, camara2, duracionMovimiento));
        }
    }

    void BackButton()
    {
        if (!enTransicion)
        {
            StartCoroutine(VolverACamaraInicial(camara1, duracionMovimiento));
        }
    }

    void QuitButton()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    IEnumerator InterpolarCamara(Camera cam1, Camera cam2, float duracion)
    {
        enTransicion = true;

        Vector3 posicionInicial = basePosition;
        Quaternion rotacionInicial = baseRotation;

        Vector3 posicionObjetivo = cam2.transform.position;
        Quaternion rotacionObjetivo = cam2.transform.rotation;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            float easedT = EaseInOutQuad(t);

            basePosition = Vector3.Lerp(posicionInicial, posicionObjetivo, easedT);
            baseRotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, easedT);

            yield return null;
        }

        basePosition = posicionObjetivo;
        baseRotation = rotacionObjetivo;

        enTransicion = false;
    }

    IEnumerator VolverACamaraInicial(Camera cam1, float duracion)
    {
        enTransicion = true;

        Vector3 posicionActual = basePosition;
        Quaternion rotacionActual = baseRotation;

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            float easedT = EaseInOutQuad(t);

            basePosition = Vector3.Lerp(posicionActual, posicionInicialCam1, easedT);
            baseRotation = Quaternion.Slerp(rotacionActual, rotacionInicialCam1, easedT);

            yield return null;
        }

        basePosition = posicionInicialCam1;
        baseRotation = rotacionInicialCam1;

        enTransicion = false;
    }

    float EaseInOutQuad(float t)
    {
        return t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }

    IEnumerator HandheldEffect()
    {
        while (true)
        {
            float time = Time.time * handHeldFrequency;

            Vector3 offsetPosition = new Vector3(
                Mathf.PerlinNoise(time, 0) - 0.5f,
                Mathf.PerlinNoise(0, time) - 0.5f,
                Mathf.PerlinNoise(time, time) - 0.5f
            ) * handHeldIntensity;

            Quaternion offsetRotation = Quaternion.Euler(
                (Mathf.PerlinNoise(time, 0) - 0.5f) * handHeldIntensity * 5f,
                (Mathf.PerlinNoise(0, time) - 0.5f) * handHeldIntensity * 5f,
                (Mathf.PerlinNoise(time, time) - 0.5f) * handHeldIntensity * 5f
            );

            // Apply handheld effect on top of the base position and rotation
            camara1.transform.position = basePosition + offsetPosition;
            camara1.transform.rotation = baseRotation * offsetRotation;

            yield return null;
        }
    }
}

public class MouseEvents : MonoBehaviour
{
    private System.Action funcion;
    private AudioSource hoverSound;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    public float scaleIncrease = 1.1f;
    public float animationDuration = 0.2f; // Duration of the hover animation
    private Coroutine scaleCoroutine;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    public void SetFunction(System.Action nuevaFuncion, AudioSource sonidoHover)
    {
        funcion = nuevaFuncion;
        hoverSound = sonidoHover;
    }

    void OnMouseEnter()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleButton(true));

        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }

    void OnMouseExit()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleButton(false));
    }

    void OnMouseDown()
    {
        funcion?.Invoke();
    }

    private IEnumerator ScaleButton(bool scaleUp)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = scaleUp ? originalScale * scaleIncrease : originalScale;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = scaleUp ?
            originalPosition + transform.TransformDirection(new Vector3(-originalScale.x * (scaleIncrease - 1) / 2, -originalScale.y * (scaleIncrease - 1) / 2, 0)) :
            originalPosition;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            transform.localScale = Vector3.Lerp(startScale, endScale, smoothT);
            transform.position = Vector3.Lerp(startPosition, endPosition, smoothT);

            yield return null;
        }

        transform.localScale = endScale;
        transform.position = endPosition;
    }
}