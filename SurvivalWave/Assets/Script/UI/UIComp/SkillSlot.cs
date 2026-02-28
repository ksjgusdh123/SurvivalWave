using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] Image skillImage;
    [SerializeField] TextMeshProUGUI levelText;
    public int skillId { get; private set; }
    public async Task SetSkillData(SkillData skillData)
    {
        skillId = skillData.skillId;
        skillImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Texture/" + skillData.imagePath).Task;
        ChangeLevel(1);
        cg.alpha = 1;
    }

    public void ChangeLevel(int level)
    {
        levelText.text = "LV " + level.ToString();
    }
}
