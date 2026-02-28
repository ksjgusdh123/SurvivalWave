using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainGameUI : UIBase
{
    PlayerInput input;
    MainGameUI()
    {
        type = EUIType.Main;
    }
    private void Awake()
    {
        input = FindFirstObjectByType<PlayerInput>();
    }

    public override void Show()
    {
        base.Show();
        input.SwitchCurrentActionMap("Player");
        SoundManager.GetInstance().UnPauseBGM();
        Utility.MouseCursorOnOff(false);
    }

    public override void Hide()
    {
        base.Hide();
        input.SwitchCurrentActionMap("UI");
        SoundManager.GetInstance().PauseBGM();
        Utility.MouseCursorOnOff(true);
    }
}
