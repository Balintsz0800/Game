using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] public Scene StartScene;
    public GameObject DeathScreen;
    public GameObject Player;
    public GameObject Camera;

    void Start()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(StartScene);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Instantiate(Player, new Vector3(0, 3, 0), Quaternion.identity);
        Player.SetActive(true);
        Camera.SetActive(false);
    }

    public void MainMenu()
    {
        DeathScreen.SetActive(false);
        SceneManager.UnloadSceneAsync(StartScene);
        SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.SetActive(false);
        Camera.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
