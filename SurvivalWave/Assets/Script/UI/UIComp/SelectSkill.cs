using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image skillImage;
    [SerializeField] private TMP_Text skillName;
    [SerializeField] private TMP_Text skillDescription;

    private int skillId;
    private Action<int> onClick;

    public void ConnetEvent(Action<int> uiAction)
    {
        onClick = uiAction;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke(skillId));
    }   

    public void SetSkillData(SkillData skill)
    {
        skillId = skill.skillId;
        int skillLevel = GameManager.GetInstance().GetSkillLevel(skillId);
        if (skillLevel <= 0)
        {
            skillName.text = skill.skillName + "_New";
            skillLevel = 0;
        }
        else 
        {
            skillName.text = skill.skillName + skillLevel.ToString();
        }

        string str = skill.description;
        string result = str.Replace("_", (skill.increaseDamageRatio * (skillLevel + 1) * 100).ToString());
        skillDescription.text = result; 
        //if (skill.icon != null) skillImage.sprite = skill.icon; 
    }
}
