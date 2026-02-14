using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : UIBase
{
    [SerializeField] private SelectSkill[] options;
    LevelUpUI()
    {
        type = EUIType.LevelUp;
    }

    private void Awake()
    {
        for (int i = 0; i < options.Length; ++i)
        {
            options[i].ConnetEvent(OnPickSkill);
        }
    }

    public void ShowOptions()
    {
        List<SkillData> skills = SkillDataManager.GetInstance().PickRandomSkill(3);
        for (int i = 0; i < options.Length; ++i)
        {
            options[i].SetSkillData(skills[i]);
        }
    }
    void OnPickSkill(int skillId)
    {
        UIManager.GetInstance().Show(EUIType.Main);
    }

    public override void Show()
    {
        Time.timeScale = 0f;
        base.Show();
        ShowOptions();
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
}
