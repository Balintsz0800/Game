using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    
    private Slider slider;
    public TMP_Text stamina;
    
    public GameObject playerState;
    public float drainStamina = 5f;
    public float regenStamina = 3f;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    

    // Update is called once per frame
    void Update()
    {
        PlayerStatics statics = PlayerStatics.Instance;
        PlayerMovement movement = PlayerMovement.Instance;
        
        float fillValue = statics.currentStamina / statics.maxStamina;
        slider.value = fillValue;
        
        stamina.text = "Stamina: " + statics.currentStamina + "/" + statics.maxStamina;
        
        if (Input.GetKey(KeyCode.LeftShift) && movement.isMoving && statics.currentStamina > 0f)
        {
            statics.currentStamina -= drainStamina * Time.deltaTime;
            statics.currentStamina = Mathf.Max(statics.currentStamina, 0f);
            UpdateUI();
        }
        else if (statics.currentStamina <= statics.maxStamina)
        {
            statics.currentStamina += regenStamina * Time.deltaTime;
            statics.currentStamina = Mathf.Min(statics.currentStamina, statics.maxStamina);
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Space)&& movement.isGrounded && statics.currentStamina > 0f)
        {
            statics.currentStamina -= drainStamina;
            statics.currentStamina = Mathf.Max(statics.currentStamina, 0f);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        PlayerStatics statics = PlayerStatics.Instance;
        stamina.text ="Stamina: " + Mathf.FloorToInt(statics.currentStamina) + "/" + statics.maxStamina;
    }
}