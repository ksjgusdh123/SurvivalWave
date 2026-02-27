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
    }

    public override void Hide()
    {
        base.Hide();
        input.SwitchCurrentActionMap("UI");
    }
}
