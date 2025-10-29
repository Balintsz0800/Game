using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen;
    private bool isDead = false;
    public static DeathScreen Instance;
    
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
