using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] Image skillImage;
    [SerializeField] TextMeshProUGUI levelText;
    public int skillId { get; private set; }
    public void SetSkillData(SkillData skillData)
    {
        skillId = skillData.skillId;
        skillImage.sprite = Resources.Load<Sprite>("Kita");
        ChangeLevel(1);
        cg.alpha = 1;
    }

    public void ChangeLevel(int level)
    {
        levelText.text = "LV " + level.ToString();
    }
}
