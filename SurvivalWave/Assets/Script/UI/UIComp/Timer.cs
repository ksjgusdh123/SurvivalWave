using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    float survivalTime;

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();    
    }
    void Update()
    {
        survivalTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
