using System.Collections.Generic;
using UnityEngine;

public enum EUIType
{
    Main,
    LevelUp
}

public class UIManager : Singleton<UIManager>
{
    Dictionary<EUIType, UIBase> uiDic = new Dictionary<EUIType, UIBase>();

    protected override void Awake()
    {
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
}
