using System.Collections.Generic;
using UnityEngine;

public enum EUIType
{
    Main,
    LevelUp
}

public class UIManager : Singleton<UIManager>
{
    SkillPanel skillPanel;
    Dictionary<EUIType, UIBase> uiDic = new Dictionary<EUIType, UIBase>();

    protected override void Awake()
    {
        skillPanel = FindAnyObjectByType<SkillPanel>();
        UIBase[] uiList = FindObjectsByType<UIBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var ui in uiList)
        {
            EUIType type = ui.type;
            uiDic.Add(type, ui);
        }
    }

    public void Show(EUIType type)
    {
        HideAll();
        uiDic[type].Show();
    }

    public void Hide(EUIType type)
    {
        uiDic[type].Hide();
    }

    public void HideAll()
    {
        foreach (var ui in uiDic.Values)
        {
            ui.Hide();
        }
    }
    public void UpdateSkillPanel(SkillData skillData, int level)
    {
        skillPanel.UpdateSkillPanel(skillData, level);
    }
}
