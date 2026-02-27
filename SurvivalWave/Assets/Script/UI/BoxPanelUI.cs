using UnityEngine;

public class BoxPanelUI : UIBase
{
    [SerializeField] RandomSkillImage itemImage;
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

        boxAnimation.pickRandomItem -= PickRandomItemAnimation;
        boxAnimation.pickRandomItem += PickRandomItemAnimation;
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
        itemImage.gameObject.SetActive(false);
        boxAnimation?.Hide();
        Time.timeScale = 1f;
    }
    public void StartShakeBox()
    {
        boxAnimation.StartBoxShakeAnimation();
    }
    public void StopShakeBox()
    {
        boxAnimation.FinishBoxShake();
    }
    public void StartBoxAnimation()
    {
        boxAnimation.StartBoxAnimation();
    }
    void PickRandomItemAnimation()
    {
        animator.Play("RandomSkillSpawn", 0, 0f);
        itemImage.gameObject.SetActive(true);
        itemImage.PickRandomSkill();
    }
}
