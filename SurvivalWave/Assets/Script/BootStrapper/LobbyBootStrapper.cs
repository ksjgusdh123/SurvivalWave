using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LobbyBootStrapper : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Game";  

    private async void Start()
    {
        await ProjectilePool.GetInstance().InitLoadAsset();
        await ParticlePool.GetInstance().InitLoadAsset();
        await MonsterPool.GetInstance().InitLoadAsset();
        await ItemPool.GetInstance().InitLoadAsset();
        await SceneManager.LoadSceneAsync(nextSceneName);
    }
}
