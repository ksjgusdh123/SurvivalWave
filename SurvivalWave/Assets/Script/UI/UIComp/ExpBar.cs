using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    Slider bar;
    PlayerStat stat;
    bool isChangeGage;
    float targetValue;
    float startValue;
    float timer;
    float duration = 1f;
    void Start()
    {
        bar = GetComponent<Slider>();
        stat = Player.playerTransform.gameObject.GetComponent<PlayerStat>();
        stat.ChangeExp -= ChangeGage;
        stat.ChangeExp += ChangeGage;
    }
    void ChangeGage()
    {
        isChangeGage = true;
        startValue = bar.value;
        targetValue = stat.exp / stat.maxExp;
        timer = 0;
    }

    private void Update()
    {
        if (!isChangeGage) return;
        if (targetValue < startValue)
        {
            startValue = 0f;
        }

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        float value = Mathf.Lerp(startValue, targetValue, t);
        bar.value = value;

        if(timer >= duration)
        {
            isChangeGage = false;
        }
    }
}
