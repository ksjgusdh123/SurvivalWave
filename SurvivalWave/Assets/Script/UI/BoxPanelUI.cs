using UnityEngine;

public class BoxPanelUI : UIBase
{
    BoxAnimation boxAnimation;
    Animator animator;
    BoxPanelUI()
    {
        type = EUIType.BoxPanel;
    }
    private void Awake()
    {
        boxAnimation = FindAnyObjectByType<BoxAnimation>();
        animator = GetComponent<Animator>();
    }
    public override void Show()
    {
        Time.timeScale = 0f;
        base.Show();
        animator.Play("OpenAnim", 0, 0f);
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }
    public void EndBoxPanelUIAnimation()
    {
        boxAnimation.StartBoxAnimation();
    }
}
