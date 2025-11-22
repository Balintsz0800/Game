using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    [SerializeField] public float timer; 
    public TMP_Text text;
    public Enemy enemy;
    private int lastMinute;
    
    public float damageMultiplier = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = timer.ToString();
    }

    public void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int minutes = (int)timer / 60;
        
        if (minutes > lastMinute)
        {
            lastMinute = minutes;
            damageMultiplier += 1.05f;
        }
        
        int seconds = (int)timer % 60;
        text.text = minutes + ":" + seconds.ToString("00");
    }
}
