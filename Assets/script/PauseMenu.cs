using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel; // 👈 добавили

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false); // 👈 важно

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false); // 👈 важно

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    // 🎛️ ОТКРЫТЬ SETTINGS
    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void ToggleSound(bool isOn)
    {
        AudioListener.volume = isOn ? 1f : 0f;
    }

    // 🔙 НАЗАД В ПАУЗУ
    public void BackToPause()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}