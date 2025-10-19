using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public TMP_Text healthText;

    public void UpdateHealthText(float health)
    {
        healthText.text = health.ToString();
    }
    
    
}
