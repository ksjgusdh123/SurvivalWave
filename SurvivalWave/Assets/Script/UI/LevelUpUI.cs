using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelUpUI : UIBase
{
    Animator animator;
    [SerializeField] private SelectSkill[] options;

    LevelUpUI()
    {
        type = EUIType.LevelUp;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < options.Length; ++i)
        {
            options[i].ConnetEvent(OnPickSkill);
        }
    }

    public async void ShowOptions()
    {
        List<SkillData> skills = SkillDataManager.GetInstance().PickRandomSkill(3);
        for (int i = 0; i < options.Length; ++i)
        {
            await options[i].SetSkillData(skills[i]);
        }
    }
    void OnPickSkill(int skillId)
    {
        GameManager.GetInstance().PickLevelUpUI(skillId);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        UIManager.GetInstance().Show(EUIType.Main);
    }

    public override void Show()
    {
        Time.timeScale = 0f;
        base.Show();
        ShowOptions();
        animator.Play("OpenAnim", 0, 0f);
        SoundManager.GetInstance().PlaySFX(SFXType.LevelUp);
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
}
