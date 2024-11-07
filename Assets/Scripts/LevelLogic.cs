using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private GameObject pauseMenuUI;

    public void ContinueGame()
    {
        Debug.Log("Continuar");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameplayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void GoToMenu()
    {
        Debug.Log("Salir");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
