using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timer; 
    public TMP_Text text;
    public Enemy enemy;
    private int lastMinute;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int minutes = (int)timer / 60;
        
        if (minutes > lastMinute)
        {
            lastMinute = minutes;
            enemy.EnemyBuff();
        }
        
        int seconds = (int)timer % 60;
        text.text = minutes + ":" + seconds.ToString("00");
    }
}
