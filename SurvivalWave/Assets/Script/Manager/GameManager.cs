using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerStat playerStat;
    PlayerSkillHandler skillHandler;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitBeforeScene()
    {
        SkillDataManager.GetInstance().LoadData();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitAfterScene()
    {
        UIManager.GetInstance().Show(EUIType.Main);
    }

    protected override void Awake()
    {
        skillHandler = FindFirstObjectByType<PlayerSkillHandler>();
        playerStat = FindFirstObjectByType<PlayerStat>();
    }

    public void PickLevelUpUI(int skillId)
    {
        skillHandler.LevelUp(skillId);
    }

    public void UpgradeAbility(StatType type, float amount)
    {
        switch (type)
        {
            case StatType.Attack:
            {
                playerStat.attack = amount * 10f;   
            }
            break;
        }
    }
}
