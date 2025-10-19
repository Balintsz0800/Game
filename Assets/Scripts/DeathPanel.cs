using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public GameObject deathScreen;
    
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (PlayerStatics.Instance.currentHealth <= 0)
        {
            deathScreen.SetActive(false);
        }
    }
}
