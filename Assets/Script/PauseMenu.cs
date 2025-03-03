using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pauseMenu; // El objeto del menú de pausa
    public GameObject gameUI; // El UI del juego (se ocultará cuando se pausa)
    public bool isPaused = false; // Estado de pausa (activado o desactivado)

    void Update()
    {
        // Detectar si se presiona la tecla "Esc" para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // Método para alternar el estado del menú de pausa
    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            ResumeGame(); // Si está pausado, reanudar el juego
        }
        else
        {
            PauseGame(); // Si no está pausado, pausar el juego
        }
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Habilitar el menú de pausa
        gameUI.SetActive(false); // Deshabilitar la interfaz del juego (opcional)
        Time.timeScale = 0f; // Detener el tiempo en el juego
        isPaused = true; // Establecer que el juego está pausado
    }

    // Método para reanudar el juego
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Deshabilitar el menú de pausa
        gameUI.SetActive(true); // Habilitar la interfaz del juego
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        isPaused = false; // Establecer que el juego no está pausado
    }

    // Método para salir del juego (opcional)
    public void QuitGame()
    {
        Debug.Log("Salir del juego...");
        Application.Quit(); // Salir de la aplicación (en el editor de Unity se detiene la ejecución)
    }
}
