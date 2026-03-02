using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    private void Awake()
    {
        //SkillDataManager.GetInstance().LoadData();
       
    }
    private void Start()
    {
        ProjectilePool.GetInstance().InstantiatePrefab();
        ParticlePool.GetInstance().InstantiatePrefab();
        MonsterPool.GetInstance().InstantiatePrefab();
        ItemPool.GetInstance().InstantiatePrefab();
        DamageTextPool.GetInstance().StartGameScene();
        SoundManager.GetInstance().PlayBGM(BGMType.Game);
        UIManager.GetInstance().Show(EUIType.Main);
    }
}
