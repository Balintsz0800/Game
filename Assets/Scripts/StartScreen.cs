using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject Player;
    public GameObject Camera;
    public GameObject SecCamera;

    void Start()
    {
        startScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        Instantiate(Player, new Vector3(0, 3, 0), Quaternion.identity);
        Player.SetActive(true);
        Camera.SetActive(false);
        SecCamera.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
