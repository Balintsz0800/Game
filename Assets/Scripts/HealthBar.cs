using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    private Slider slider;
    public Text health;
    
    public GameObject playerState;
    private float currentHealth, maxHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerStatics>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerStatics>().maxHealth;
        
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
        
        health.text = currentHealth + "/" + maxHealth;
    }
}
