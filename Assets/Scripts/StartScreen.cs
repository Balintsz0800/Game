using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject SettingsPanel;
    void Start()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}