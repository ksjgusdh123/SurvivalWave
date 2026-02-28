using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class RandomSkillImage : MonoBehaviour
{
    [SerializeField] Image skillImage;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] GameObject closeButton;
    List<Sprite> sprites = new List<Sprite>();
    int skillCount;

    WaitForSecondsRealtime rollingTotalCoroutine;
    WaitForSecondsRealtime rollingItemCoroutine;
    float rollingDuration = 3.2f;
    float rollingInterval = 0.1f;

    int targetIdx;
    public bool isRolling;

    private async void Awake()
    {
        rollingTotalCoroutine = new WaitForSecondsRealtime(rollingDuration);
        rollingItemCoroutine = new WaitForSecondsRealtime(rollingInterval);
        skillCount = (int)SkillItemType.Max;
        for (int i = 0; i < skillCount; ++i)
        {
            var data = SkillDataManager.GetInstance().GetSkillData(i);
            var sprite = await Addressables.LoadAssetAsync<Sprite>("Texture/" + data.imagePath).Task;
            sprites.Add(sprite);
        }
    }
    private void OnDisable()
    {
        if (skillName) skillName.gameObject.SetActive(false);
    }
    private void Update()
    {

    }
    public void PickRandomSkill()
    {
        targetIdx = Random.Range(0, skillCount);

        isRolling = true;
        StartCoroutine(RollingItemImage());
        StartCoroutine(TotalRollingManage());
    }
    IEnumerator TotalRollingManage()
    {
        yield return rollingTotalCoroutine;
        isRolling = false;
        GameManager.GetInstance().PickLevelUpUI(targetIdx);
        skillImage.sprite = sprites[targetIdx];
        skillName.gameObject.SetActive(true);
        skillName.text = SkillDataManager.GetInstance().GetSkillData(targetIdx).skillName;
        closeButton.SetActive(true);
    }
    IEnumerator RollingItemImage()
    {
        int idx = 0;
        while(isRolling)
        {
            yield return rollingItemCoroutine;
            if (!isRolling) yield break;
            skillImage.sprite = sprites[idx++ % skillCount];
        }
    }
}
