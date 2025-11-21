using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer; 
    public bool isDay = true;
    public TMP_Text text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = timer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 60f)
        {
            Enemy.EnemyBuff();
        }
        timer += Time.deltaTime;
        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;
        text.text = minutes + ":" + seconds.ToString("00");
    }
}
