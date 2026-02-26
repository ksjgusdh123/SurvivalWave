using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    private void Awake()
    {
        //SkillDataManager.GetInstance().LoadData();
        ProjectilePool.GetInstance().InstantiatePrefab();
        ParticlePool.GetInstance().InstantiatePrefab();
        MonsterPool.GetInstance().InstantiatePrefab();
        ItemPool.GetInstance().InstantiatePrefab();
    }
    private void Start()
    {
        UIManager.GetInstance().Show(EUIType.Main);
    }
}
