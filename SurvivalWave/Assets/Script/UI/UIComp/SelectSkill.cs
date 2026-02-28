using System;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

    public async Task SetSkillData(SkillData skill)
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

        string[] parts = skill.description.Split('_');
        string[] separators = { (skill.increaseDamageRatio * (skillLevel + 1) * 100).ToString(),
            (skill.decreaseLaunchInterval * (skillLevel + 1) * 100).ToString() };

        string result = parts[0];

        for (int i = 1; i < parts.Length; i++)
        {
            if (i - 1 < separators.Length)
            {
                result += separators[i - 1];
            }
            result += parts[i];
        }

        string str = skill.description;
        //string result = str.Replace("_", (skill.increaseDamageRatio * (skillLevel + 1) * 100).ToString());
        skillDescription.text = result;
        skillImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Texture/" + skill.imagePath).Task;
    }
}
