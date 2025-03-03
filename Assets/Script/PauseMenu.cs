using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pauseMenu; // El objeto del men� de pausa
    public GameObject gameUI; // El UI del juego (se ocultar� cuando se pausa)
    public bool isPaused = false; // Estado de pausa (activado o desactivado)

    void Update()
    {
        // Detectar si se presiona la tecla "Esc" para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // M�todo para alternar el estado del men� de pausa
    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame(); // Si est� pausado, reanudar el juego
        }
        else
        {
            PauseGame(); // Si no est� pausado, pausar el juego
        }
    }

    // M�todo para pausar el juego
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Habilitar el men� de pausa
        gameUI.SetActive(false); // Deshabilitar la interfaz del juego (opcional)
        Time.timeScale = 0f; // Detener el tiempo en el juego
        isPaused = true; // Establecer que el juego est� pausado
    }

    // M�todo para reanudar el juego
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Deshabilitar el men� de pausa
        gameUI.SetActive(true); // Habilitar la interfaz del juego
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        isPaused = false; // Establecer que el juego no est� pausado
    }

    // M�todo para salir del juego (opcional)
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit(); // Salir de la aplicaci�n (en el editor de Unity se detiene la ejecuci�n)
    }
}
