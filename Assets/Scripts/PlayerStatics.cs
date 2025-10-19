using UnityEngine;

public class PlayerStatics : MonoBehaviour
{ 
    public static PlayerStatics Instance { get; set;}
    
    public float currentHealth;
    public float maxHealth;
    
    public float currentStamina;
    public float maxStamina;
    
    
    private void Awake()
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

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            DeathScreen.Instance.ShowDeathScreen();
        }
    }
}