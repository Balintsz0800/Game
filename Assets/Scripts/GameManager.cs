using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagger : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform PlayerSpawn;
    public GameObject DeathScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Instantiate(PlayerPrefab, PlayerSpawn.position, PlayerSpawn.rotation);
        DeathScreen.SetActive(false);
    }

    // Update is called once per frame
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
