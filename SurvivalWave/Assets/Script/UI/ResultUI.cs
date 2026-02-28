using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : UIBase
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Button closeButton;
    ResultUI()
    {
        type = EUIType.Result;
    }
    private void Awake()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => Application.Quit());
    }
    public override void Show()
    {
        base.Show();
        float survivalTime = GameManager.GetInstance().survivalTime;
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
