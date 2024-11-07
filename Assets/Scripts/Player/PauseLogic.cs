using UnityEngine;
using UnityEngine.InputSystem;

public class PauseLogic : MonoBehaviour
{
    private bool isPaused = false;

    private PlayerInput playerInput;
    private InputAction pauseAction;

    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private GameObject pauseMenuUI;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
    }

    private void OnEnable()
    {
        pauseAction.performed += OnPausePerformed;
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePerformed;
        pauseAction.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            gameplayUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            gameplayUI.SetActive(true);
            pauseMenuUI.SetActive(false);
        }
    }
}