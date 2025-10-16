using UnityEngine;

public class PlayerState : MonoBehaviour
{ 
    public static PlayerState Instance { get; set;}
    
    public float currentHealth;
    public float maxHealth;

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
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentHealth > 0)
            {
                currentHealth -=10;
            }
        }
    }
}