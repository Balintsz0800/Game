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
    

    // Update is called once per frame
    void Update()
    {}
}