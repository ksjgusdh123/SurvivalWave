using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyBootStrapper : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Game";
    [SerializeField] Slider progressbar;
    float multiNum = 0.25f;
    private async void Start()
    {
        float total = 0f;
        await ProjectilePool.GetInstance().Init(p => { SetUIProgress(ref total, p); });
        await ParticlePool.GetInstance().Init(p => { SetUIProgress(ref total, p); });
        await MonsterPool.GetInstance().Init(p => { SetUIProgress(ref total, p); });
        await ItemPool.GetInstance().Init(p => { SetUIProgress(ref total, p); });
        SkillDataManager.GetInstance().LoadData();
        await SceneManager.LoadSceneAsync(nextSceneName);
    }
    void SetUIProgress(ref float total, float value)
    {
        float finalValue = total + value * multiNum;
        progressbar.value = finalValue;
        if(value >= 1f)
        {
            total += multiNum;
        }
    }
}
