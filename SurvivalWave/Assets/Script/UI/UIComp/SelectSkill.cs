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

        skillName.text = skill.skillName;
        skillDescription.text = skill.description; 
        //if (skill.icon != null) skillImage.sprite = skill.icon; 
    }
}
