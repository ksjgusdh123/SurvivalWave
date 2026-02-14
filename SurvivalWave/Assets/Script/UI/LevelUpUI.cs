using UnityEngine;

public class LevelUpUI : UIBase
{
    LevelUpUI()
    {
        type = EUIType.LevelUp;
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
}
